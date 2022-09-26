namespace Boilerplate.Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;

public interface IResetPasswordService
{
    void SendResetPasswordMailToUser(string email, string resetPasswordToken);
}