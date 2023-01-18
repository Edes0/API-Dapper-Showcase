namespace Mimbly.Infrastructure.Security.Configurations;

/// <summary>
/// Class <c>AzureAdConfiguration</c> purpose is to handle the
/// extraction of AzureAd variables from appsettings.
/// </summary>
public class AzureAdConfiguration
{
    public string? Instance { get; set; }
    public string? ClientId { get; set; }
    public string? TenantId { get; set; }
    public string? ClientSecret { get; set; }
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
}