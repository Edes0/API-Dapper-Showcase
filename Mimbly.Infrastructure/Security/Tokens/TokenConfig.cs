namespace Mimbly.Infrastructure.Security.Tokens;

public class TokenConfig
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public long AccessTokenExpiration { get; set; }
    public long RefreshTokenExpiration { get; set; }
    public string SecretKey { get; set; } = null!;
    public long ResetPasswordTokenExpiration { get; set; }
    public long InviteNewUserTokenExpiration { get; set; }
}