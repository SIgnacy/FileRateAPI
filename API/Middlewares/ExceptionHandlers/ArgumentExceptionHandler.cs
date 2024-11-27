using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares.ExceptionHandlers;

public class ArgumentExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not ArgumentException argumentException) return false;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "BadRequest",
            Detail = argumentException.Message
        };

        httpContext.Response.StatusCode = (int)details.Status;

        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

        return true;
    }
}
