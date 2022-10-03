namespace Mimbly.Application.Commands.Identity.RefreshAccessToken;

using System.Security.Claims;
using Common.Interfaces;
using Contracts.Dtos;
using CoreServices.Exceptions;
using Infrastructure.Security.Tokens.Interfaces;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, RefreshAccessTokenVm>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly ITokenHandler _tokenHandler;

    public RefreshAccessTokenCommandHandler(IIdentityRepository identityRepository, ITokenHandler tokenHandler)
    {
        _identityRepository = identityRepository;
        _tokenHandler = tokenHandler;
    }

    public async Task<RefreshAccessTokenVm> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userRefreshTokens = await _identityRepository.GetRefreshTokensByUserId(Guid.Parse(request.UserId));

        _ = userRefreshTokens.ToList().Contains(request.RefreshToken) ? true : throw new BadRequestException("No such refresh token exist in database");
        _ = _tokenHandler.ValidateJwtToken(request.RefreshToken) ? true : throw new AuthorizationException("The provided refresh token is not valid");

        var user = await _identityRepository.GetUserByUserId(Guid.Parse(request.UserId));

        _ = user != null ? true : throw new NotFoundException("No user connected to the refresh token found in database");

        var userClaims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

        var accessToken = _tokenHandler.GenerateJsonWebToken(userClaims);
        var refreshToken = _tokenHandler.GenerateJsonWebToken(userClaims);

        await _identityRepository.UpdateStoredRefreshToken(request.RefreshToken, refreshToken, Guid.Parse(request.UserId));

        return new RefreshAccessTokenVm()
        {
            TokenData = new TokenDataDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        };
    }
}