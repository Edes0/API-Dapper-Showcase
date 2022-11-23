namespace Mimbly.Infrastructure.AAD;

using System.ComponentModel.DataAnnotations;
using Microsoft.Graph;

public class UserInviteModel
{
    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }

    [Required]
    public string? DisplayName { get; set; }

    public string? GroupId { get; set; }

    public Contact? Contact { get; set; }
}

public class Contact
{
    public string? JobTitle { get; set; }

    [Phone]
    public string? MobilePhone { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}

