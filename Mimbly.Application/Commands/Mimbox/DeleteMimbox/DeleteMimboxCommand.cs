namespace Mimbly.Application.Commands.Mimbox.DeleteMimbox;

using MediatR;

public class DeleteMimboxCommand : IRequest
{
    public Guid Id { get; init; }
    public string Authorization { get; set; } = null!; //TODO: WHat here? Put in into a baseclass?
}