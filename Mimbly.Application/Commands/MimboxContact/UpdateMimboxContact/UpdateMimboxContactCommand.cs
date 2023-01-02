namespace Mimbly.Application.Commands.MimboxContact.UpdateMimboxContact;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxContact;

public class UpdateMimboxContactCommand : IRequest
{
    public Guid Id { get; set; }
    public UpdateMimboxContactRequestDto UpdateMimboxContactRequest { get; set; } = null!;
}