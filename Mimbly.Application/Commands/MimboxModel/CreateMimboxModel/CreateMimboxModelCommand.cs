namespace Mimbly.Application.Commands.MimboxModel.CreateMimboxModel;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxModel;
using Mimbly.Domain.Entities;

public class CreateMimboxModelCommand : IRequest<MimboxModel>
{
    public CreateMimboxModelRequestDto CreateMimboxModelRequest { get; set; } = null!;
}
