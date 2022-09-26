namespace Boilerplate.Application.Commands.Identity.LoginUser;

using Contracts.Dtos;

public class LoginUserVm
{
    public LoginUserVm() => TokenResponse = new TokenDataDto();

    public TokenDataDto TokenResponse { get; set; }
}