using Microsoft.AspNetCore.Diagnostics;

namespace Template.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // 1. If the error is a KeyNotFoundException, return a 404 Not Found
            if (exception is KeyNotFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsJsonAsync(new { Error = exception.Message }, cancellationToken);

                return true; // "true" means we successfully handled the error!
            }

            // 2. If it's any other random crash, return a 500 Internal Server Error
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { Error = "An unexpected error occurred in the server." }, cancellationToken);

            return true;
        }
    }
}