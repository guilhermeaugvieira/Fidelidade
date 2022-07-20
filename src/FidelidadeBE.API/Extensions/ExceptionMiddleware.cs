using System.Net;

namespace FidelidadeBE.API.Extensions;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch(Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var exceptionObject = new
        {
            success = false,
            errors = new
            {
                message = e.Message,
                innerException = e.InnerException,
            },
        };

        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(exceptionObject);
    }
}