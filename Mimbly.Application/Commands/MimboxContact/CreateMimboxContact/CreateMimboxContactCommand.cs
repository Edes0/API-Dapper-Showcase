namespace Mimbly.Application.Commands.MimboxContact.CreateMimboxContact;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxContact;
using Mimbly.Domain.Entities;

public class CreateMimboxContactCommand : IRequest<MimboxContact>
{
    public CreateMimboxContactRequestDto CreateMimboxContactRequest { get; set; } = null!;
}
