namespace Mimbly.Application.Commands.Identity.RegisterUser;

using Mimbly.Application.Contracts.Requests.Identity;
using MediatR;

public class RegisterUserCommand : IRequest
{
    public RegisterUserRequestDto RegisterUserRequestDto { get; set; } = null!;
}