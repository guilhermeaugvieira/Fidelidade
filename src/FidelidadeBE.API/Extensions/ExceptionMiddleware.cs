using System.Net;

namespace FidelidadeBE.API.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch
        {
            await HandleExceptionAsync(httpContext);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        return Task.CompletedTask;
    }
}