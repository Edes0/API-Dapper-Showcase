namespace Boilerplate.Application.Commands.Emails;

using Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;
using Infrastructure.Security.Tokens.Interfaces;
using MediatR;

public class ResetUserPasswordByEmailCommandHandler : IRequestHandler<ResetUserPasswordByEmailCommand>
{
    private readonly ITokenHandler _tokenHandler;
    private readonly IResetPasswordService _resetPasswordService;

    public ResetUserPasswordByEmailCommandHandler(
        ITokenHandler tokenHandler,
        IResetPasswordService resetPasswordService)
    {
        _tokenHandler = tokenHandler;
        _resetPasswordService = resetPasswordService;
    }
    public async Task<Unit> Handle(ResetUserPasswordByEmailCommand request, CancellationToken cancellationToken)
    {
        await request.ResetUserPasswordRequest.Validate();
        var resetPasswordToken = _tokenHandler.GenerateResetPasswordToken(request.ResetUserPasswordRequest.Email);
        _resetPasswordService.SendResetPasswordMailToUser(request.ResetUserPasswordRequest.Email, resetPasswordToken);

        return Unit.Value;
    }
}