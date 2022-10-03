namespace Mimbly.Application.Commands.Identity.LogoutUserAll;

using System;
using MediatR;

public class LogoutUserAllCommand : IRequest
{
    public Guid UserId { get; set; }
}