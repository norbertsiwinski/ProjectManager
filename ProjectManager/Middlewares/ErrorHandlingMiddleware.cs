using ProjectManager.Application.Exceptions;
using ProjectManager.Domain.Exceptions;

namespace ProjectManager.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (DomainException domainException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(domainException.Message);
        }
        catch (UnauthorizedAccessException unauthorizedException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(unauthorizedException.Message);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(exception.Message);
        }
    }
}