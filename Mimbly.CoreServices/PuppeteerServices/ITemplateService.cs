namespace Mimbly.CoreServices.PuppeteerServices;

public interface ITemplateService
{
    Task<string> RenderAsync<TModel>(string templateFileName, TModel model);
}
