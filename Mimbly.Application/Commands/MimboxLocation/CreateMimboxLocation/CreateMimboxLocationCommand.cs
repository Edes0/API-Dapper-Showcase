namespace Mimbly.Application.Commands.MimboxLocation.CreateMimboxLocation;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;
using Mimbly.Domain.Entities;

public class CreateMimboxLocationCommand : IRequest<MimboxLocation>
{
    public CreateMimboxLocationRequestDto CreateMimboxLocationRequest { get; set; } = null!;
}