namespace Boilerplate.Application.Contracts.Requests.Identity;

public class LogoutSingleDeviceRequestDto
{
    public string RefreshToken { get; set; } = null!;
}