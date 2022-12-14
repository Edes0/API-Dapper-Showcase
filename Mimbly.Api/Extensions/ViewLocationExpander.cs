namespace Mimbly.Api.Extensions;

using Microsoft.AspNetCore.Mvc.Razor;

public class ViewLocationExpander : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        viewLocations = viewLocations.Select(s => s.Replace("Views", "DocumentTemplates"));
        return viewLocations;
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {

    }
}
