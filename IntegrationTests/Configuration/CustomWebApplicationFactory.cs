using System.Data.Common;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Respawn;
using Testcontainers.MsSql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Configuration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MsSqlContainer _dbContainer = null!;
    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;
    private readonly ILogger<CustomWebApplicationFactory> _logger;
    private static string? _appConnectionString;
    private TestDatabaseSettings _testSettings;

    public HttpClient HttpClient { get; private set; } = null!;

    public CustomWebApplicationFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<CustomWebApplicationFactory>();
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.testing.json", optional: false)
            .Build();
        
        _testSettings = new TestDatabaseSettings();
        configuration.GetSection("TestDatabase").Bind(_testSettings);
    }

    public async Task InitializeAsync()
    {
        try
        {
            _logger.LogInformation("[FACTORY] Starting container initialization...");
            
            await InitializeContainerAsync();
            await InitializeDatabaseConnectionAsync();
            await InitializeRespawnerAsync();
            InitializeHttpClientAsync();
            ConfigureAppConnectionString();
            
            _logger.LogInformation("[FACTORY] Factory initialization completed successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FACTORY] Error during initialization");
            throw;
        }
    }

    public new async Task DisposeAsync()
    {
        try
        {
            _logger.LogInformation("[FACTORY] Disposing factory...");
            await _dbContainer.DisposeAsync();
            await _dbConnection.DisposeAsync();
            _logger.LogInformation("[FACTORY] Factory disposed successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FACTORY] Error during disposal");
        }
    }

    public async Task ResetDatabaseAsync()
    {
        try
        {
            _logger.LogInformation("[FACTORY] Resetting database...");
            await _respawner.ResetAsync(_dbConnection);
            _logger.LogInformation("[FACTORY] Database reset completed!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FACTORY] Error resetting database");
            throw;
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        try
        {
            _logger.LogInformation("[FACTORY] Configuring WebHost...");
            
            ConfigureConnectionString(builder);
            ConfigureLogging(builder);
            
            _logger.LogInformation("[FACTORY] WebHost configured successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FACTORY] Error configuring WebHost");
            throw;
        }
    }

    private void ConfigureConnectionString(IWebHostBuilder builder)
    {
        var testConnectionString = _appConnectionString ?? _testSettings.BuildConnectionString("localhost", TestDatabaseSettings.DefaultSqlServerPort);
        _logger.LogInformation("[FACTORY] Using connection string: {ConnectionString}", testConnectionString);
        
        builder.UseSetting("ConnectionStrings:DefaultConnection", testConnectionString);
    }

    private void ConfigureLogging(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Information);
        });
    }

    private async Task InitializeRespawnerAsync()
    {
        try
        {
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer,
                SchemasToInclude = new[] { "dbo" }
            });
            _logger.LogInformation("[FACTORY] Respawner created successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FACTORY] Error creating Respawner");
            throw;
        }
    }

    private async Task InitializeContainerAsync()
    {
        _logger.LogInformation("[FACTORY] Creating MSSQL container...");
        _dbContainer = CreateMsSqlContainer();

        _logger.LogInformation("[FACTORY] Starting MSSQL container...");
        await _dbContainer.StartAsync();
        _logger.LogInformation("[FACTORY] Container started successfully!");

        _logger.LogInformation("[FACTORY] Waiting for container to be fully ready...");
        await Task.Delay(TimeSpan.FromSeconds(10));
        _logger.LogInformation("[FACTORY] Container is ready!");
    }

    private MsSqlContainer CreateMsSqlContainer()
    {
        return new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword(_testSettings.DbPassword)
            .WithPortBinding(TestDatabaseSettings.DefaultSqlServerPort)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(TestDatabaseSettings.DefaultSqlServerPort))
            .Build();
    }

    private async Task InitializeDatabaseConnectionAsync()
    {
        var connectionString = CreateContainerConnectionString();
        _logger.LogInformation("[FACTORY] Testcontainers connection string: {ConnectionString}", connectionString);

        _logger.LogInformation("[FACTORY] Opening SQL connection...");
        _dbConnection = new SqlConnection(connectionString);
        await _dbConnection.OpenAsync();
        _logger.LogInformation("[FACTORY] SQL connection opened successfully!");
    }

    private string CreateContainerConnectionString()
    {
        var host = _dbContainer.Hostname;
        var port = _dbContainer.GetMappedPublicPort(TestDatabaseSettings.DefaultSqlServerPort);
        var connectionString = _testSettings.BuildConnectionString(host, port);
        _logger.LogInformation("[FACTORY] Container host: {Host}, port: {Port}", host, port);
        return connectionString;
    }

    private void InitializeHttpClientAsync()
    {
        _logger.LogInformation("[FACTORY] Creating HttpClient...");
        HttpClient = CreateClient();
        _logger.LogInformation("[FACTORY] HttpClient ready!");
    }

    private void ConfigureAppConnectionString()
    {
        try
        {
            var appConnectionString = CreateContainerConnectionString();
            _appConnectionString = appConnectionString;
            _logger.LogInformation("[FACTORY] App connection string set: {ConnectionString}", appConnectionString);
            
            Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", appConnectionString);
            _logger.LogInformation("[FACTORY] Environment variable set for connection string");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FACTORY] Error configuring app connection string");
        }
    }
} 