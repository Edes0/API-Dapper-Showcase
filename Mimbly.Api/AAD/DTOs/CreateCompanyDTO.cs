namespace Mimbly.Api.AAD.DTOs;

public class CreateCompanyDTO
{
    public InvitedUser user { get; set; }
    public CompanyModel company { get; set; }
}
