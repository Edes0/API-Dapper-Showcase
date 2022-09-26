namespace Boilerplate.Infrastructure.ExternalServices.MailServices;

using Application.Common.Interfaces.ExternalServices.MailServices;
using CoreServices.Configurations;
using Interfaces.ExternalServices.MailServices;
using Microsoft.Extensions.Options;

public class ResetPasswordService : IResetPasswordService
{
    private readonly IMailService _mailService;
    private readonly IOptions<FrontendApplicationConfig> _frontendApplicationConfig;

    public ResetPasswordService(
        IMailService mailService,
        IOptions<FrontendApplicationConfig> frontendApplicationConfig
    )
    {
        _mailService = mailService;
        _frontendApplicationConfig = frontendApplicationConfig;
    }

    public void SendResetPasswordMailToUser(string email, string resetPasswordToken)
    {
        var resetPasswordLink = _frontendApplicationConfig.Value.EmailInviteBaseUrl + resetPasswordToken;

        //Fetching Email Body Text from EmailTemplate File.
        var filePath = Path.GetFullPath("Templates/ResetPassword.html");
        var instaPath = Path.GetFullPath("Templates/Assets/instagramlogo.png");
        var twPath = Path.GetFullPath("Templates/Assets/twitterlogo.jpeg");
        var fbPath = Path.GetFullPath("Templates/Assets/fblogo.jpeg");
        var str = new StreamReader(filePath);
        var mailText = str.ReadToEnd();
        str.Close();

        // Base64 convert images in order for them to correctly display in email.
        var instaArray = File.ReadAllBytes(instaPath);
        var instaBase64String = Convert.ToBase64String(instaArray);

        var fbArray = File.ReadAllBytes(fbPath);
        var fbBase64String = Convert.ToBase64String(fbArray);

        var twArray = File.ReadAllBytes(twPath);
        var twBase64String = Convert.ToBase64String(twArray);

        // Replacing content in file.
        mailText = mailText.Replace("[resetpassword-link]", resetPasswordLink);
        mailText = mailText.Replace("[instalogo]", instaBase64String);
        mailText = mailText.Replace("[fblogo]", fbBase64String);
        mailText = mailText.Replace("[twlogo]", twBase64String);
        var thread = new Thread(() => _mailService.SendMailSingleLanguage
        (
            email,
            "Reset password",
            mailText
        ));
        thread.Start();
    }
}