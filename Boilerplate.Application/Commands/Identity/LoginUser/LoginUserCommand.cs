namespace Boilerplate.Application.Commands.Identity.LoginUser;

using Boilerplate.Application.Contracts.Requests.Identity;
using MediatR;

public class LoginUserCommand : IRequest<LoginUserVm>
{
    public LoginUserRequestDto LoginUserRequestDto { get; set; } = null!;
}