namespace Boilerplate.Application.Commands.CreateBoilerPlate;

using Contracts.RequestDtos;
using MediatR;

public class CreateBoilerplateCommand : IRequest
{
    public CreateBoilerplateRequestDto CreateBoilerplateRequest { get; set; } = null!;
}