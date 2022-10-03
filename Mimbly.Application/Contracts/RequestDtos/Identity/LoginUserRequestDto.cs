namespace Mimbly.Application.Contracts.Requests.Identity;

public class LoginUserRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}