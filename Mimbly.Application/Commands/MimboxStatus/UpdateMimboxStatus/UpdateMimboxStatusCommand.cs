namespace Mimbly.Application.Commands.MimboxStatus.UpdateMimboxStatus;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;

public class UpdateMimboxStatusCommand : IRequest
{
    public UpdateMimboxStatusRequestDto UpdateMimboxStatusRequest { get; set; } = null!;
    public Guid Id { get; set; }
}