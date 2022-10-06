namespace Mimbly.Application.Commands.CreateMimbly;

using Contracts.RequestDtos;
using MediatR;

public class CreateMimboxCommand : IRequest
{
    public CreateMimboxRequestDto CreateMimboxRequest { get; set; } = null!;
}