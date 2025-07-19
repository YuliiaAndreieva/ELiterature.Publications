namespace Core.Common;

/// <summary>
/// Result pattern for better error handling
/// </summary>
/// <typeparam name="T">The type of the result value</typeparam>
public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    public Exception? Exception { get; }

    private Result(bool isSuccess, T? value = default, string? error = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Exception = exception;
    }

    public static Result<T> Success(T value) => new(true, value);
    public static Result<T> Failure(string error) => new(false, error: error);
    public static Result<T> Failure(Exception exception) => new(false, exception: exception);
    public static Result<T> Failure(string error, Exception exception) => new(false, error: error, exception: exception);
}

/// <summary>
/// Non-generic Result for operations that don't return a value
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public Exception? Exception { get; }

    private Result(bool isSuccess, string? error = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Exception = exception;
    }

    public static Result Success() => new(true);
    public static Result Failure(string error) => new(false, error);
    public static Result Failure(Exception exception) => new(false, exception: exception);
    public static Result Failure(string error, Exception exception) => new(false, error, exception);
} 