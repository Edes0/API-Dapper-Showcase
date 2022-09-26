namespace Boilerplate.Application.Commands.Identity.ChangePassword;

using System;
using Boilerplate.Application.Contracts.Requests.Identity;
using MediatR;

public class ChangePasswordCommand : IRequest
{
    public Guid UserId { get; set; }
    public ChangePasswordRequestDto ChangePasswordData { get; set; } = null!;
}