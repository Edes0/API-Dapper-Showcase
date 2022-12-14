namespace Mimbly.Application.Commands.MimboxLocation.UpdateMimboxLocation;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class UpdateMimboxLocationCommand : IRequest
{
    public UpdateMimboxLocationRequestDto UpdateMimboxLocationRequest { get; set; } = null!;
    public Guid Id { get; set; }
}
