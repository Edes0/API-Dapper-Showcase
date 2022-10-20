namespace Mimbly.Api.Extensions;

using PuppeteerSharp;

public static class PuppeteerExtensions
{
    private static string _executablePath;
    public static async Task PreparePuppeteerAsync(this IServiceCollection service,
        IWebHostEnvironment hostingEnvironment)
    {
        // Downloads & Installs a chromium browser.
        var downloadPath = Path.Join(hostingEnvironment.ContentRootPath, @"\puppeteer");
        var browserOptions = new BrowserFetcherOptions { Path = downloadPath };
        var browserFetcher = new BrowserFetcher(browserOptions);
        _executablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);
        await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
    }

    public static string ExecutablePath => _executablePath;
}


