namespace Mimbly.Application.Commands.Mimbox.CreateMimbox;

using MediatR;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class CreateMimboxCommand : IRequest
{
    public CreateMimboxRequestDto CreateMimboxRequest { get; set; } = null!;
    public string Authorization { get; set; } = null!; //TODO: WHat here? Put in into a baseclass?
}