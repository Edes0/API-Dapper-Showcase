namespace Boilerplate.Infrastructure.ExternalServices.MailServices;

using Application.Common.Interfaces.ExternalServices.MailServices;
using CoreServices.Configurations;
using CoreServices.DateTimeHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

public class MailService : IMailService, IDisposable
{
    private readonly string _domainName;
    private readonly ILogger<MailService> _logger;
    private readonly RestClient _restClient;

    public MailService(IOptions<MailGunConfig> mailGunConfig, ILogger<MailService> logger)
    {
        _domainName = mailGunConfig.Value.DomainName;
        var apiKey = mailGunConfig.Value.ApiKey;
        var baseUrl = mailGunConfig.Value.BaseUrl;
        _logger = logger;
        _restClient = new RestClient
        {
            Options = { BaseUrl = new Uri(baseUrl) },
            Authenticator = new HttpBasicAuthenticator("api", apiKey)
        };
    }

    public async Task SendMailSingleLanguage(string toEmail, string subject, string body)
    {
        var request = new RestRequest();
        request.AddParameter("domain", _domainName, ParameterType.UrlSegment);
        request.AddParameter("from", "Company Name <noreply@mail.companyname.se>");
        request.AddParameter("to", toEmail);
        request.AddParameter("subject", subject);
        request.AddParameter("html", body);
        request.Method = Method.Post;

        var result = await _restClient.ExecuteAsync(request);

        LogEmailResultMessage(result, subject, toEmail);
    }

    private void LogEmailResultMessage(RestResponseBase result, string subject, string to)
    {
        if (result.IsSuccessful)
        {
            _logger.LogInformation("Email with subject '{Subject}' sent to {To} at:" +
                                   "{Timestamp}", subject, to, TimeStampGenerator.GenerateTimeStamp());
        }
        else
        {
            _logger.LogError("Email with subject '{Subject}' to {To}, failed to be sent at:" +
                             "{Timestamp}" +
                             "Failed with error message '{ErrorMessage}' and status description '{StatusDescription}'",
                    subject, to, TimeStampGenerator.GenerateTimeStamp(), result.ErrorMessage, result.StatusDescription);
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}