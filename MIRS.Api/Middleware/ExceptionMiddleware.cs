using System.Net;
using System.Text.Json;
using MIRS.Api.Errors;
using MIRS.Application.Exceptions;
using MIRS.Domain.Exceptions;

namespace MIRS.Api.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        ApiResponse response;
        int statusCode;

        switch (ex)
        {
            case DomainException domainEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiException(statusCode, domainEx.Message);
                break;

            case NotFoundException notFoundEx:
                statusCode = (int)HttpStatusCode.NotFound;
                response = new ApiException(statusCode, notFoundEx.Message);
                break;

            case ValidationException validationEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiValidationErrorResponse(validationEx.Errors);
                break;

            case ForbiddenAccessException forbiddenEx:
                statusCode = (int)HttpStatusCode.Forbidden;
                response = new ApiException(statusCode, forbiddenEx.Message);
                break;

            case AuthenticationFailedException authEx:
                statusCode = (int)HttpStatusCode.Unauthorized;
                response = new ApiException(statusCode, authEx.Message);
                break;

            case ConflictException conflictEx:
                statusCode = (int)HttpStatusCode.Conflict;
                response = new ApiException(statusCode, conflictEx.Message);
                break;

            case MIRS.Application.Exceptions.ApplicationException appEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiException(statusCode, appEx.Message);
                break;

            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                response = new ApiResponse(statusCode);
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                response = new ApiException(
                    statusCode,
                    "Internal server error",
                    _env.IsDevelopment() ? ex.StackTrace : null
                );
                break;
        }

        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(
            response,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        await context.Response.WriteAsync(json);
    }
}