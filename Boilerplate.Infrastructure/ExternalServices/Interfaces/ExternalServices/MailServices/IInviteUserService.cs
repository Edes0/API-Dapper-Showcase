namespace Boilerplate.Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;

public interface IInviteUserService
{
    void SendInviteMailToUser(string email, string invitePasswordToken);
}