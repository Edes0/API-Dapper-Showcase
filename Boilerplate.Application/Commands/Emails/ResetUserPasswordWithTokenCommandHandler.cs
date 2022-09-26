namespace Boilerplate.Application.Commands.Emails;

using System.IdentityModel.Tokens.Jwt;
using Common.Interfaces;
using CoreServices.Exceptions;
using Infrastructure.Security;
using Infrastructure.Security.Tokens.Interfaces;
using MediatR;

public class ResetUserPasswordWithTokenCommandHandler : IRequestHandler<ResetUserPasswordWithTokenCommand>
{
    private readonly ITokenHandler _tokenHandler;
    private readonly IIdentityRepository _identityRepository;

    public ResetUserPasswordWithTokenCommandHandler(
        ITokenHandler tokenHandler,
        IIdentityRepository identityRepository)
    {
        _tokenHandler = tokenHandler;
        _identityRepository = identityRepository;
    }
    public async Task<Unit> Handle(ResetUserPasswordWithTokenCommand request, CancellationToken cancellationToken)
    {
        var isTokenValid = _tokenHandler.ValidateJwtToken(request.ResetUserPasswordWithTokenRequest.Token);

        _ = !isTokenValid ? throw new BadRequestException("The provided token is either incorrect or expired") : true;
        var jsonToken = _tokenHandler.DecodeToken(request.ResetUserPasswordWithTokenRequest.Token);
        var emailClaim = jsonToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email);

        if (emailClaim == null)
        {
            throw new BadRequestException("The provided token is either incorrect or expired");
        }

        var user = await _identityRepository.GetUserByEmail(emailClaim.Value);

        if (user == null)
        {
            throw new BadRequestException($"There is no user with email {emailClaim.Value} from the token. " +
                                          "Something has gone wrong.");
        }

        await request.ResetUserPasswordWithTokenRequest.Validate();

        var hashedPassword = PasswordHandler.HashPassword(request.ResetUserPasswordWithTokenRequest.Password);

        await _identityRepository.ChangeUserPassword(user.Id, hashedPassword);
        return Unit.Value;
    }
}