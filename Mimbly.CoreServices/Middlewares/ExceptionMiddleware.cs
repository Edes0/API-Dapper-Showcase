namespace Mimbly.CoreServices.Middlewares;

using System.Net;
using System.Text.Json;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private static ILogger<ExceptionMiddleware> _logger = null!;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
        _logger = LoggerFactory.Create(builder => builder.AddSimpleConsole(opt =>
        {
            opt.IncludeScopes = true;
            opt.TimestampFormat = "hh:mm:ss ";
            opt.ColorBehavior = LoggerColorBehavior.Enabled;
        })).CreateLogger<ExceptionMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(
            new
            {
                error = "Something went wrong. Please try again.",
            });

        switch (exception)
        {
            case BadRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(
                    new
                    {
                        error = exception.Message
                    });

                ExceptionLogging.LogExceptionWarning(_logger, code, exception, "BadRequestException");
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(
                    new
                    {
                        error = exception.Message
                    });

                ExceptionLogging.LogExceptionWarning(_logger, code, exception, "NotFoundException");
                break;
            case ForbiddenException:
                code = HttpStatusCode.Forbidden;
                result = JsonSerializer.Serialize(
                    new
                    {
                        error = exception.Message
                    });

                ExceptionLogging.LogExceptionWarning(_logger, code, exception, "ForbiddenException");
                break;
            case FailedDependencyException:
                code = HttpStatusCode.FailedDependency;
                result = JsonSerializer.Serialize(
                    new
                    {
                        error = exception.Message,
                        innerException = exception.InnerException?.Message
                    });

                ExceptionLogging.LogExceptionError(_logger, code, exception, "FailedDependency");
                break;
            case AuthorizationException:
                code = HttpStatusCode.Unauthorized;
                result = JsonSerializer.Serialize(
                    new
                    {
                        error = exception.Message
                    });

                ExceptionLogging.LogExceptionWarning(_logger, code, exception, "UnauthorizedException");
                break;
            default:
                ExceptionLogging.LogExceptionError(_logger, code, exception, "InternalServerError");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}