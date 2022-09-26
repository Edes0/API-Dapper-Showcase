namespace Boilerplate.Infrastructure.Security.Tokens.Interfaces;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public interface ITokenHandler
{
    string GenerateJsonWebToken(IEnumerable<Claim> userClaims);
    string GenerateRefreshToken(string userEmail);
    string GetUserIdFromAccessToken(string accessToken);
    string GenerateResetPasswordToken(string email);
    JwtSecurityToken DecodeToken(string token);
    bool ValidateJwtToken(string token);
    string BuildPasswordResetTokenForNewUser(string email);
}