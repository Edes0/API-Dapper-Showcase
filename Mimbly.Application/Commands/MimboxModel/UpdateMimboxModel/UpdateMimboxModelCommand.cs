namespace Mimbly.Application.Commands.MimboxModel.UpdateMimboxModel;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxModel;

public class UpdateMimboxModelCommand : IRequest
{
    public UpdateMimboxModelRequestDto UpdateMimboxModelRequest { get; set; } = null!;
    public Guid Id { get; set; }
}