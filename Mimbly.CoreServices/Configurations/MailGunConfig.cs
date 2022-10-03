namespace Mimbly.CoreServices.Configurations;

public class MailGunConfig
{
    public string ApiKey { get; set; } = null!;
    public string BaseUrl { get; set; } = null!;
    public string DomainName { get; set; } = null!;
}