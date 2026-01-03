using MIRS.Application.Exceptions;
using MIRS.Domain.Exceptions;

namespace MIRS.Web.Middleware;

public class WebExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<WebExceptionMiddleware> _logger;

    public WebExceptionMiddleware(RequestDelegate next, ILogger<WebExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

            if (context.Response.HasStarted)
            {
                _logger.LogWarning("The response has already started, the error handler will not be executed.");
                throw;
            }

            int statusCode = ex switch
            {
                DomainException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                ForbiddenAccessException => StatusCodes.Status403Forbidden,
                AuthenticationFailedException => StatusCodes.Status401Unauthorized,
                ConflictException => StatusCodes.Status409Conflict,
                MIRS.Application.Exceptions.ApplicationException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.Clear();
            context.Response.StatusCode = statusCode;

            // Store the exception message in a way that the Error page can access it if needed
            // However, for Razor Pages, it's often better to redirect or re-execute
            // Re-executing allows us to show a friendly page based on the status code
            
            // We use ReExecute if we want to show the status code page we will create
            context.Items["ExceptionMessage"] = ex.Message;
            
            // Re-throwing to let UseExceptionHandler or UseStatusCodePages handle it might be tricky with custom logic
            // But we want to show the user a specific page.
            
            // If it's a 404 from NotFoundException, we might want to go to a 404 page
            // If it's a 400 from DomainException, maybe stay on same page with an error message? 
            // Usually for global handling, redirecting to a status page is cleanest.
            
            var path = $"/StatusCode/{statusCode}";
            context.Request.Path = path;
            await _next(context);
        }
    }
}
