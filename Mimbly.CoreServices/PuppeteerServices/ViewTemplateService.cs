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

    public async Task<string> RenderAsync<TViewModel>(string filename, TViewModel viewModel)
    {
        // Creates a context for the mvc action
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProvider
        };
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

        // Finds the View for {filename}
        var viewResult = _viewEngine.FindView(actionContext, filename, false);

        // Adds a model for the View
        var viewDictionary = new ViewDataDictionary<TViewModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = viewModel
        };
        var tempDataDictionary = new TempDataDictionary(httpContext, _tempDataProvider);

        // string for the html output
        await using var outputWriter = new StringWriter();

        if (!viewResult.Success)
        {
            throw new KeyNotFoundException(
                $"Could not render the HTML, because {filename} template does not exist");
        }

        try
        {
            // Generate context
            var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary,
                tempDataDictionary, outputWriter, new HtmlHelperOptions());

            // Render generated context and return it
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
