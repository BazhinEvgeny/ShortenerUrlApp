using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebDotNetApplication.Middlewares;

public class ExceptionHandlingMiddleware : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var ex = context.Exception;
        context.Result = new ObjectResult(ex.Message)
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}