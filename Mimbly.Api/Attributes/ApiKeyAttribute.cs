namespace Mimbly.Api.Attributes;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if ((context.HttpContext.User.Identity != null) && context.HttpContext.User.Identity.IsAuthenticated)
        { await next(); }

        if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var suppliedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "No api key was not provided"
            };

            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = appSettings.GetValue<string>("ApiKey");

        if (!apiKey.Equals(suppliedApiKey, StringComparison.Ordinal))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Supplied api key is not valid"
            };

            return;
        }

        await next();
    }
}