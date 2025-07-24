namespace Data.Repositories.Interfaces;

public interface IRawSqlRepository
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
    
    Task<IEnumerable<dynamic>> GroupByAsync(string sql, object? parameters = null);
    
    Task<int> ExecuteAsync(string sql, object? parameters = null);
} 