using API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests;

[Collection("Unit:GlobalExceptionFilter")]
public class GlobalExceptionFilterTests
{
    [Fact(DisplayName = "GlobalExceptionFilter handles unexpected exceptions")]
    public void GlobalExceptionFilter_HandlesUnexpectedExceptions()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalExceptionFilter>>();
        var filter = new GlobalExceptionFilter(loggerMock.Object);
        var exception = new InvalidOperationException("Test exception");
        var context = CreateExceptionContext(exception);

        // Act
        filter.OnException(context);

        // Assert
        Assert.True(context.ExceptionHandled);
        Assert.NotNull(context.Result);
        var jsonResult = Assert.IsType<JsonResult>(context.Result);
        Assert.Equal(500, jsonResult.StatusCode);
        
        var response = jsonResult.Value;
        Assert.NotNull(response);
        
        // Use reflection to access anonymous object properties
        var responseType = response.GetType();
        var errorProperty = responseType.GetProperty("error");
        var timestampProperty = responseType.GetProperty("timestamp");
        var pathProperty = responseType.GetProperty("path");
        var methodProperty = responseType.GetProperty("method");
        
        Assert.NotNull(errorProperty);
        Assert.NotNull(timestampProperty);
        Assert.NotNull(pathProperty);
        Assert.NotNull(methodProperty);
        
        Assert.Equal("An unexpected error occurred.", errorProperty.GetValue(response));
        Assert.NotNull(timestampProperty.GetValue(response));
        Assert.NotNull(pathProperty.GetValue(response));
        Assert.NotNull(methodProperty.GetValue(response));
    }

    [Fact(DisplayName = "GlobalExceptionFilter logs exceptions")]
    public void GlobalExceptionFilter_LogsExceptions()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalExceptionFilter>>();
        var filter = new GlobalExceptionFilter(loggerMock.Object);
        var exception = new ArgumentNullException("paramName");
        var context = CreateExceptionContext(exception);

        // Act
        filter.OnException(context);

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unexpected error occurred")),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact(DisplayName = "GlobalExceptionFilter preserves exception details in log")]
    public void GlobalExceptionFilter_PreservesExceptionDetailsInLog()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalExceptionFilter>>();
        var filter = new GlobalExceptionFilter(loggerMock.Object);
        var exception = new DivideByZeroException("Division by zero");
        var context = CreateExceptionContext(exception);

        // Act
        filter.OnException(context);

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unexpected error occurred")),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    private static ExceptionContext CreateExceptionContext(Exception exception)
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/test";
        httpContext.Request.Method = "GET";

        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = exception
        };

        return exceptionContext;
    }
} 