﻿namespace Mimbly.Api.Controllers.v1;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimbly.Api.Extensions;
using Mimbly.CoreServices.PuppeteerServices;
using Mimbly.Domain.DocumentModels;
using PuppeteerSharp;
using PuppeteerSharp.Media;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class DocumentController : Controller
{
    private readonly ITemplateService _templateService;

    public DocumentController(ITemplateService templateService) => _templateService = templateService;

    // Proof of concept.
    [HttpGet]
    [Route("GetMonthlyReport")]
    public async Task<IActionResult> GetMonthlyReport(bool download)
    {
        var model = new ReportModel
        {
            Company = new Company
            {
                Name = "E CORP"
            },
            Stats = new Stats
            {
                MoneySaved = "10 203",
                PlasticSaved = 132,
                WaterSaved = 44
            },
            BestMimboxes = new List<Address>
            {
                new Address
                {
                    Country = "Sweden",
                    City = "Borås",
                    StreetAddress = "Helvetesgatan 1",
                    PostCode = "465 54"
                },
                new Address
                {
                    Country = "Sweden",
                    City = "Stockholm",
                    StreetAddress = "Kalkstensgatan 55",
                    PostCode = "123 11"
                },
                new Address
                {
                    Country = "Sweden",
                    City = "Göteborg",
                    StreetAddress = "Doktor Eggs Gata 1",
                    PostCode = "414 42"
                },
            }
        };

        var html = await _templateService.RenderAsync("MonthlyReportTemplate", model);

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

        if (download)
        {
            return File(pdfContent, "application/pdf", $"Report-{model.Company.Name}-{model.Created}.pdf");
        }

        return File(pdfContent, "application/pdf");
    }
}
