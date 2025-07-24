using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Data.Repositories.Interfaces;

namespace Data.Repositories;

public class RawSqlRepository : IRawSqlRepository
{
    private readonly string _connectionString;
    private readonly ILogger<RawSqlRepository> _logger;

    public RawSqlRepository(IConfiguration configuration, ILogger<RawSqlRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        try
        {
            _logger.LogInformation("Executing query: {Sql}", sql);
            using var connection = CreateConnection();
            var result = await connection.QueryAsync<T>(sql, parameters);
            _logger.LogInformation("Query executed successfully, returned {Count} records", result.Count());
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing query: {Sql}", sql);
            throw;
        }
    }

    public async Task<IEnumerable<dynamic>> GroupByAsync(string sql, object? parameters = null)
    {
        try
        {
            _logger.LogInformation("Executing group by query: {Sql}", sql);
            using var connection = CreateConnection();
            var result = await connection.QueryAsync(sql, parameters);
            _logger.LogInformation("Group by query executed successfully, returned {Count} groups", result.Count());
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing group by query: {Sql}", sql);
            throw;
        }
    }

    public async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        try
        {
            _logger.LogInformation("Executing non-query command: {Sql}", sql);
            using var connection = CreateConnection();
            var result = await connection.ExecuteAsync(sql, parameters);
            _logger.LogInformation("Non-query command executed successfully, affected {Rows} rows", result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing non-query command: {Sql}", sql);
            throw;
        }
    }
} 