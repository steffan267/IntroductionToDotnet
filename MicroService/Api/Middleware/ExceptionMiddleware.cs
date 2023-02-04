using System.Net;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middleware;

/*
 *   https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-6.0
 *   Here we define a Testory for generating the appropriate responses we expect. There are frameworks to handle this
 *   fluently from the Program.cs, but they also have their limitations. 
 */
public static class ExceptionMiddleware
{
    public static async Task Invoke(HttpContext context)
    {
        var scope = context.RequestServices.CreateScope().ServiceProvider;
        var logger = scope.GetService<ILoggerFactory>()!.CreateLogger(typeof(ExceptionMiddleware));
        var exceptionContext = context.Features.Get<IExceptionHandlerFeature>();
        
        switch (exceptionContext?.Error)
        {
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync("Not found");
                break;
            default:
                logger.LogError("Unhandled exception was thrown {@Exception}", exceptionContext!.Error);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Body = new MemoryStream(); //haven't checked if this works yet -> but we should not have a body on unhandled exceptions for security reasions
                await context.Response.WriteAsync("No exception handler");
                break;
        }

        //TODO: Fancy response modelling here
        
        //Unknown caller does not deserve any information! (a known caller would be able to check logs)
        // await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionContext?.Error.Message ?? "",Formatting.Indented));
    }
}