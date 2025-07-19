using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var requestPath = context.HttpContext.Request.Path;
        var method = context.HttpContext.Request.Method;

        _logger.LogError(exception, "Unexpected error occurred while processing {Method} {Path}", 
            method, requestPath);

        var errorResponse = new
        {
            error = "An unexpected error occurred.",
            timestamp = DateTime.UtcNow,
            path = requestPath,
            method = method
        };

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
} 