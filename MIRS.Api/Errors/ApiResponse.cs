namespace MIRS.Api.Errors;

public class ApiResponse
{
    public int StatusCode { get; }
    public string Message { get; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    private static string GetDefaultMessage(int statusCode) =>
        statusCode switch
        {
            400 => "Bad request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Resource not found",
            500 => "An unexpected error occurred",
            _ => null
        };
}