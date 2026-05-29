using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ecomercio.Domain.Exceptions;

namespace Ecomercio.WebApi.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        var (statusCode, title) = exception switch
        {
            NotFoundException ex => (StatusCodes.Status404NotFound, ex.Message),
            ValidationException ex => (StatusCodes.Status400BadRequest, "Validation failed"),
            DomainException ex => (StatusCodes.Status422UnprocessableEntity, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Instance = httpContext.Request.Path
            },
            cancellationToken);

        return true;
    }
}