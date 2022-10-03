namespace Mimbly.Infrastructure.Security.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Configurations;
using CoreServices.Exceptions;
using Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class TokenHandler : ITokenHandler
{
    private readonly IOptions<TokenConfig> _tokenConfig;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SigningConfigurations _signingConfigurations;

    public TokenHandler(IOptions<TokenConfig> tokenConfig, SigningConfigurations signingConfigurations)
    {
        _tokenConfig = tokenConfig;
        _tokenHandler = new JwtSecurityTokenHandler();
        _signingConfigurations = signingConfigurations;
    }

    /**
     * Generates a JSON web token.
     *
     * @param {IEnumerable<Claim>} userClaims - Contains claims to be set in the json web token.
     *
     * @returns string representing a Json web token.
     */
    public string GenerateJsonWebToken(IEnumerable<Claim> userClaims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _tokenConfig.Value.Issuer,
            _tokenConfig.Value.Audience,
            userClaims,
            expires: DateTime.Now.AddSeconds(_tokenConfig.Value.AccessTokenExpiration),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    /**
     * Generates a refresh token.
     *
     * @param {string} userEmail - The user email to be placed in the refresh token.
     *
     * @returns string representing a Json web token used as a refresh token.
     */
    public string GenerateRefreshToken(string userEmail)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Email, userEmail)
            };

        var token = new JwtSecurityToken(
            _tokenConfig.Value.Issuer,
            _tokenConfig.Value.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(_tokenConfig.Value.RefreshTokenExpiration),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public string GetUserIdFromAccessToken(string accessToken)
    {
        if (accessToken == null)
        {
            throw new AuthorizationException("No accessToken Provided");
        }

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(accessToken);

        if (jsonToken is not JwtSecurityToken jwt)
        {
            throw new AuthorizationException("Could not decode jwt");
        }

        return jwt.Subject;
    }

    public string GenerateResetPasswordToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

        var token = new JwtSecurityToken(
            _tokenConfig.Value.Issuer,
            _tokenConfig.Value.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(_tokenConfig.Value.ResetPasswordTokenExpiration),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public string BuildPasswordResetTokenForNewUser(string email)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenConfig.Value.InviteNewUserTokenExpiration);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, email),
        };

        var securityToken = new JwtSecurityToken
        (
            claims: claims,
            issuer: _tokenConfig.Value.Issuer,
            audience: _tokenConfig.Value.Audience,
            expires: accessTokenExpiration,
            signingCredentials: _signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(securityToken);
        return accessToken;
    }

    public JwtSecurityToken DecodeToken(string token)
    {
        return _tokenHandler.ReadJwtToken(token);
    }

    public bool ValidateJwtToken(string token)
    {
        try
        {
            _tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _tokenConfig.Value.Issuer,
                ValidAudience = _tokenConfig.Value.Audience,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                        (_tokenConfig.Value.SecretKey))
            }, out _);
        }
        catch
        {
            return false;
        }

        return true;
    }
}