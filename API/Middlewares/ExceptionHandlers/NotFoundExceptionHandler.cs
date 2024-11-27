using Domain.Exceptions.NotFoundException;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException memberNotFoundException) return false;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "NotFound",
            Detail = memberNotFoundException.Message
        };

        httpContext.Response.StatusCode = (int)details.Status;

        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

        return true;
    }
}