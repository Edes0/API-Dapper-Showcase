namespace Mimbly.Business.Helpers.AD;

using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Mimbly.Business.Interfaces.AD;
using Mimbly.Infrastructure.Security.Configurations;

public class GraphService : IGraphService
{
    private IConfiguration _config;

    public GraphService(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Method <c>GraphServiceClient</c> instantiates a GraphAPI client
    /// using Azure Ad credentials from appsettings.
    /// </summary>
    /// <returns>returns a <c>GraphServiceClient</c>.</returns>
    public GraphServiceClient GetClient()
    {

        var azureAdSection = _config.GetSection("AzureAd");
        var azureAd = azureAdSection.Get<AzureAdConfiguration>();

        var scopes = new[] { "https://graph.microsoft.com/.default" };
        var tenantId = azureAd.TenantId;
        var clientId = azureAd.ClientId;
        var clientSecret = azureAd.ClientSecret;

        var clientSecretCredentials = new ClientSecretCredential(tenantId, clientId, clientSecret);

        var graphClient = new GraphServiceClient(clientSecretCredentials, scopes);

        return graphClient;
    }
}
