namespace Boilerplate.Application.Commands.Identity.RegisterUser;

using Boilerplate.Application.Contracts.Requests.Identity;
using MediatR;

public class RegisterUserCommand : IRequest
{
    public RegisterUserRequestDto RegisterUserRequestDto { get; set; } = null!;
}