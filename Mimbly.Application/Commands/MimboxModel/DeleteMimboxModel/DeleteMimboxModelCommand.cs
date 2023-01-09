namespace Mimbly.Application.Commands.MimboxModel.DeleteMimboxModel;

using MediatR;

public class DeleteMimboxModelCommand : IRequest
{
    public Guid Id { get; init; }
}

