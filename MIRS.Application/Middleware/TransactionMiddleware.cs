using Microsoft.AspNetCore.Http;
using MIRS.Domain.Interfaces;

namespace MIRS.Application.Middleware;

public class TransactionMiddleware
{
    private readonly RequestDelegate _next;

    public TransactionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        await _next(context);

        if (IsWriteRequest(context.Request.Method) && IsResponseSuccessful(context.Response.StatusCode))
        {
            await unitOfWork.CompleteAsync();
        }
    }

    private static bool IsWriteRequest(string method)
    {
        return method == HttpMethods.Post || 
               method == HttpMethods.Put || 
               method == HttpMethods.Delete || 
               method == HttpMethods.Patch;
    }

    private static bool IsResponseSuccessful(int statusCode)
    {
        return (statusCode >= 200 && statusCode < 300) || statusCode == 302;
    }
}
