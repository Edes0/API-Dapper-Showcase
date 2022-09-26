namespace Boilerplate.Application.Contracts.Requests.Identity;

public class ChangePasswordRequestDto
{
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}