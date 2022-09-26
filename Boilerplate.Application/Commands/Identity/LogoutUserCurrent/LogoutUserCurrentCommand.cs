namespace Boilerplate.Application.Commands.Identity.LogoutUserCurrent;

using System;
using MediatR;

public class LogoutUserCurrentCommand : IRequest
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = null!;
}