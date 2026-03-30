using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging; // We need this for ILogger!

namespace Template.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        // Inject the logger into the constructor
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is KeyNotFoundException)
            {
                // Log it as a Warning (since it's just a missing item, not a system crash)
                _logger.LogWarning("Resource not found: {Message}", exception.Message);

                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsJsonAsync(new { Error = exception.Message }, cancellationToken);

                return true;
            }

            // LOG THE REAL ERROR! This saves the stack trace to our text file!
            _logger.LogError(exception, "A critical server error occurred: {Message}", exception.Message);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { Error = "An unexpected error occurred in the server." }, cancellationToken);

            return true;
        }
    }
}