namespace Mimbly.CoreServices.Middlewares;

using System.Net;
using Exceptions;
using Microsoft.AspNetCore.Http;

public static class ControllerExceptionMiddleware
{
    public static async Task HandleControllerExceptions(HttpContext context, Func<Task> next)
    {
        await next();

        switch (context.Response.StatusCode)
        {
            case (int)HttpStatusCode.Unauthorized:
                throw new AuthorizationException("Token validation has failed. Request access denied.");

            case (int)HttpStatusCode.Forbidden:
                throw new ForbiddenException("You don't have the correct clearance to perform this action. Request access denied.");

            case (int)HttpStatusCode.NotFound:
                throw new NotFoundException("The endpoint you tried to access does not exist.");

            default:
                return;
        }
    }
}