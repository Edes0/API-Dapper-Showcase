namespace Mimbly.Application.Commands.Mimbox.UpdateMimbox;

using MediatR;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class UpdateMimboxCommand : IRequest
{
    public UpdateMimboxRequestDto UpdateMimboxRequest { get; set; } = null!;
    public string Authorization { get; set; } = null!; //TODO: Authorization
}
