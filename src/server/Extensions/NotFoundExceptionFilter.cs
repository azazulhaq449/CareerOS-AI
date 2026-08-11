using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CareerOS.Server.Extensions;

/// <summary>
/// Maps a <see cref="KeyNotFoundException"/> thrown by any repository
/// (update/delete of a resource that doesn't exist) to a 404 response,
/// registered once here instead of a try/catch in every controller action.
/// </summary>
public class NotFoundExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is KeyNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new { message = context.Exception.Message });
            context.ExceptionHandled = true;
        }
    }
}
