namespace Boilerplate.Domain.ApplicationModels.Email;

public class InvitedUserModel
{
    public string Email { get; set; } = null!;

    public string? Error { get; set; }
}