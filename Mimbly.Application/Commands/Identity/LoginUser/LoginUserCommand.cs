namespace Mimbly.Application.Commands.Identity.LoginUser;

using Mimbly.Application.Contracts.Requests.Identity;
using MediatR;

public class LoginUserCommand : IRequest<LoginUserVm>
{
    public LoginUserRequestDto LoginUserRequestDto { get; set; } = null!;
}