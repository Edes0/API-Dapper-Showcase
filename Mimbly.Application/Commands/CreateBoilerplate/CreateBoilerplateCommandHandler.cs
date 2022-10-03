namespace Mimbly.Application.Commands.CreateMimbly;

using Common.Interfaces;
using MediatR;

public class CreateMimblyCommandHandler : IRequestHandler<CreateMimblyCommand>
{
    private readonly IMimblyRepository _MimblyRepository;

    public CreateMimblyCommandHandler(IMimblyRepository MimblyRepository) => _MimblyRepository = MimblyRepository;

    public async Task<Unit> Handle(CreateMimblyCommand request, CancellationToken cancellationToken)
    {
        // Might not be required. Call this before calling the api.
        await request.CreateMimblyRequest.Validate();

        // This runs a single task. If several entities use Task.WhenAll
        await Task.Run(() => request.CreateMimblyRequest.Validate(), cancellationToken);

        return Unit.Value;
    }
}