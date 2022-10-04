namespace Mimbly.Application.Commands.CreateMimbly;

using Contracts.RequestDtos;
using MediatR;

public class CreateMimblyCommand : IRequest
{
    public CreateMimblyRequestDto CreateMimblyRequest { get; set; } = null!;
}