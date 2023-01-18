namespace Mimbly.CoreServices.PuppeteerServices;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

public class ViewTemplateService : ITemplateService
{
    private IRazorViewEngine _viewEngine;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly ILogger<ViewTemplateService> _logger;

    public ViewTemplateService(IRazorViewEngine viewEngine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider, ILogger<ViewTemplateService> logger)
    {
        _viewEngine = viewEngine;
        _serviceProvider = serviceProvider;
        _tempDataProvider = tempDataProvider;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Method <c>RenderAsync</c> takes in a razor view and model,
    /// then uses the Razor engine to create the html.
    /// </summary>
    /// <param name="viewName">The name for the Razor view.</param>
    /// <param name="viewModel">The model used in the Razor view</param>
    /// <typeparam name="TViewModel"></typeparam>
    /// <returns>Returns a string containing the html generated.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the view is not found.</exception>
    public async Task<string> RenderAsync<TViewModel>(string viewName, TViewModel viewModel)
    {
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProvider
        };

        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        var viewResult = _viewEngine.FindView(actionContext, viewName, false);

        var viewDictionary = new ViewDataDictionary<TViewModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = viewModel
        };
        var tempDataDictionary = new TempDataDictionary(httpContext, _tempDataProvider);

        await using var outputWriter = new StringWriter();

        if (!viewResult.Success)
        {
            throw new KeyNotFoundException(
                $"Could not render the HTML, because {viewName} template does not exist");
        }

        try
        {
            var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary,
                tempDataDictionary, outputWriter, new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);
            return outputWriter.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not render the HTML because of an error");
            return string.Empty;
        }
    }
}
