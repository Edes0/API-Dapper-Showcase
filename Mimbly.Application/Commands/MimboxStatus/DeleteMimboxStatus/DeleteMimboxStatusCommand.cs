namespace Mimbly.Application.Commands.MimboxStatus.DeleteMimboxStatus;

using MediatR;

public class DeleteMimboxStatusCommand : IRequest
{
    public Guid Id { get; init; }
}

