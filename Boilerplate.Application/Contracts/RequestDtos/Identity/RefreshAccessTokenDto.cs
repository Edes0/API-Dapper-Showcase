namespace Boilerplate.Application.Contracts.Requests.Identity;

public class RefreshAccessTokenDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}