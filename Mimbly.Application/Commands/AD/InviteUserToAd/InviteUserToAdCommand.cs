namespace Mimbly.Application.Commands.AD.InviteUserToAd;

using Contracts.Dtos.AD;
using MediatR;

public class InviteUserToAdCommand : IRequest<bool>
{
    public InviteUserDto InviteUserToAdRequest { get; set; }
}