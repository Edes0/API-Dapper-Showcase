namespace Boilerplate.Application.Common.Interfaces.ExternalServices.MailServices;

public interface IMailService
{
    Task SendMailSingleLanguage(string toEmail, string subject, string body);
}