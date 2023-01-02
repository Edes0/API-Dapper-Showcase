namespace Mimbly.Application.Commands.MimboxStatus.CreateMimboxStatus;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;
using Mimbly.Domain.Entities;

public class CreateMimboxStatusCommand : IRequest<MimboxStatus>
{
    public CreateMimboxStatusRequestDto CreateMimboxStatusRequest { get; set; } = null!;
}
