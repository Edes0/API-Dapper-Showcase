namespace Mimbly.Api.AAD.DTOs;

public class CreateCompanyDTO
{
    public UserInviteDTO user { get; set; }
    public CompanyModel company { get; set; }
}
