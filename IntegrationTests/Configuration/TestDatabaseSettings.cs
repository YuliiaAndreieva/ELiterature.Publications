namespace IntegrationTests.Configuration;

public class TestDatabaseSettings
{
    public const int DefaultSqlServerPort = 1433;
    
    private string Host { get; set; } = "localhost";
    private int Port { get; set; } = DefaultSqlServerPort;
    private string Database { get; set; } = "master";
    private string User { get; set; } = "sa";
    private string Password { get; set; } = "YourStrong(!)Password123";
    private bool TrustServerCertificate { get; set; } = true;
    private bool IntegratedSecurity { get; set; } = false;
    private int ConnectRetryCount { get; set; } = 3;
    private int ConnectRetryInterval { get; set; } = 10;
    private int ConnectionTimeout { get; set; } = 30;
    private int CommandTimeout { get; set; } = 30;
    public string DbPassword => Password;

    public string BuildConnectionString(string host, int port)
    {
        return $"Server={host},{port};Database={Database};User Id={User};Password={Password};TrustServerCertificate={TrustServerCertificate};Integrated Security={IntegratedSecurity};ConnectRetryCount={ConnectRetryCount};ConnectRetryInterval={ConnectRetryInterval};Connection Timeout={ConnectionTimeout};Command Timeout={CommandTimeout}";
    }
} 