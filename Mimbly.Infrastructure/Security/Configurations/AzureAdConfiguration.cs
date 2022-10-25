namespace Mimbly.Infrastructure.Security.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AzureAdConfiguration
{
    public string Instance { get; set; }
    public string ClientId { get; set; }
    public string TenantId { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
}