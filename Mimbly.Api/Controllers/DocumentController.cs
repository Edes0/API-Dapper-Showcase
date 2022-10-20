namespace Mimbly.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Mimbly.Api.Extensions;
using Mimbly.CoreServices.PuppeteerServices;
using Mimbly.Domain.DocumentModels;
using PuppeteerSharp;
using PuppeteerSharp.Media;

[ApiController]
[Route("api/v1/[controller]")]
public class DocumentController : Controller
{
    private readonly ITemplateService _templateService;

    public DocumentController(ITemplateService templateService)
    {
        _templateService = templateService;
    }

    [HttpGet]
    [Route("GetMonthlyReport")]
    public async Task<IActionResult> GetMonthlyReport()
    {
        // TODO: Remake once entities are solid & checked out
        var model = new ReportModel
        {
            Company = new Company {
                Name = "E CORP",
                Address = new Address
                {
                    Country = "United States",
                    City = "Chicago",
                    StreetAddress = "McKinley key dough street 144",
                    PostCode = "LS3 88L",
                },
            },
            Stats = new Stats
            {
                CarbonSaved = 12,
                CarbonSavedLastMonth = 11,
                PlasticSaved = 44,
                PlasticSavedLastMonth = 62,
                WaterSaved = 4580,
                WaterSavedLastMonth = 4493
            }
        };

        // Generate html from razor template
        var html = await _templateService.RenderAsync("MonthlyReportTemplate", model);

        // Launch chromium and "print out" pdf
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = PuppeteerExtensions.ExecutablePath
        });
        await using var page = await browser.NewPageAsync();
        await page.EmulateMediaTypeAsync(MediaType.Screen);
        await page.SetContentAsync(html);
        var pdfContent = await page.PdfStreamAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true
        });

        return File(pdfContent, "application/pdf", $"Report-{model.Company.Name}-{model.Created}.pdf");
    }

    public async Task<IActionResult> Preview(string templateName)
    {
        var model = new ReportModel
        {
            Company = new Company
            {
                Name = "E CORPO",
                Address = new Address
                {
                    Country = "United States",
                    City = "Chicago",
                    StreetAddress = "McKinley key dough street 144",
                    PostCode = "LS3 88L",
                },
            },
            Stats = new Stats
            {
                CarbonSaved = 12,
                CarbonSavedLastMonth = 11,
                PlasticSaved = 44,
                PlasticSavedLastMonth = 62,
                WaterSaved = 4580,
                WaterSavedLastMonth = 4493
            }
        };

        return View(templateName, model);
    }
}
