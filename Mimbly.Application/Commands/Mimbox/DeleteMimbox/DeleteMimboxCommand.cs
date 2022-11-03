namespace Mimbly.Application.Commands.Mimbox.DeleteMimbox;

using MediatR;

public class DeleteMimboxCommand : IRequest
{
    public Guid Id { get; init; }
}