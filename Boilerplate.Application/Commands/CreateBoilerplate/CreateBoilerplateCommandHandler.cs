namespace Boilerplate.Application.Commands.CreateBoilerPlate;

using Common.Interfaces;
using MediatR;

public class CreateBoilerplateCommandHandler : IRequestHandler<CreateBoilerplateCommand>
{
    private readonly IBoilerplateRepository _boilerplateRepository;

    public CreateBoilerplateCommandHandler(IBoilerplateRepository boilerplateRepository) => _boilerplateRepository = boilerplateRepository;

    public async Task<Unit> Handle(CreateBoilerplateCommand request, CancellationToken cancellationToken)
    {
        // Might not be required. Call this before calling the api.
        await request.CreateBoilerplateRequest.Validate();

        // This runs a single task. If several entities use Task.WhenAll
        await Task.Run(() => request.CreateBoilerplateRequest.Validate(), cancellationToken);

        return Unit.Value;
    }
}