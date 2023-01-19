namespace Mimbly.CoreServices.PuppeteerServices;

public interface ITemplateService
{
    Task<string> RenderAsync<TModel>(string viewName, TModel model);
}
