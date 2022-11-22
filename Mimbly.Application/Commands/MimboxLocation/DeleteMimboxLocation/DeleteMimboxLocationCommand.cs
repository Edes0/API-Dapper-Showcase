namespace Mimbly.Application.Commands.MimboxLocation.DeleteMimboxLocation;

using MediatR;

public class DeleteMimboxLocationCommand : IRequest
{
    public Guid Id { get; init; }
}
