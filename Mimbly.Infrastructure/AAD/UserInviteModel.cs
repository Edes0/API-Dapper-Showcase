namespace Mimbly.Infrastructure.AAD;

using Microsoft.Graph;

public class UserInviteModel
{
    public string? UserType { get; set; }
    public bool IsGroupAdmin { get; set; }
    public string? EmailAddress { get; set; }
    public string? DisplayName { get; set; }
    public string? GroupId { get; set; }
    public Contact? Contact { get; set; }
}

public class Contact
{
    public string? JobTitle { get; set; }
    public string? MobilePhone { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }


}

