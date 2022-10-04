namespace Mimbly.Application.Commands.Identity.ChangePassword;

using Common.Interfaces;
using CoreServices.Exceptions;
using Infrastructure.Security;
using MediatR;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IIdentityRepository _identityRepository;

    public ChangePasswordCommandHandler(IIdentityRepository identityRepository) => _identityRepository = identityRepository;

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityRepository.GetUserByUserId(request.UserId);

        if (!PasswordHandler.VerifyPassword(request.ChangePasswordData.CurrentPassword, user.Password))
        {
            throw new BadRequestException("Incorrect password provided");
        }

        var newHashedPassword = PasswordHandler.HashPassword(request.ChangePasswordData.NewPassword);

        await _identityRepository.ChangeUserPassword(request.UserId, newHashedPassword);

        return Unit.Value;
    }
}