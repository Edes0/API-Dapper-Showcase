namespace Mimbly.Application.Commands.Mimbox.CreateMimbox;

using MediatR;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.Domain.Entities;

public class CreateMimboxCommand : IRequest<Mimbox>
{
    public CreateMimboxRequestDto CreateMimboxRequest { get; set; } = null!;
}