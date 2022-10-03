namespace Mimbly.Application.Commands.Identity.LoginUser;

using System.Security.Claims;
using Common.Interfaces;
using Contracts.Dtos;
using CoreServices.Exceptions;
using Infrastructure.Security;
using Infrastructure.Security.Tokens.Interfaces;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserVm>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(IIdentityRepository identityRepository, ITokenHandler tokenHandler)
    {
        _identityRepository = identityRepository;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginUserVm> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityRepository.GetUserByEmail(request.LoginUserRequestDto.Email);

        _ = user != null ? true : throw new AuthorizationException("Incorrect email or password");

        _ = PasswordHandler.VerifyPassword(request.LoginUserRequestDto.Password, user.Password)
            ? true
            : throw new AuthorizationException("Incorrect email or password");

        var userClaims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var accessToken = _tokenHandler.GenerateJsonWebToken(userClaims);
        var refreshToken = _tokenHandler.GenerateRefreshToken(user.Email);

        await _identityRepository.StoreRefreshToken(refreshToken, user.Id);

        return new LoginUserVm
        {
            TokenResponse = new TokenDataDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        };
    }
}