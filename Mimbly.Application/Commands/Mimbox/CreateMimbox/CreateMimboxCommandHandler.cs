namespace Mimbly.Application.Commands.CreateMimbox;

using Common.Interfaces;
using MediatR;

public class CreateMimblyCommandHandler : IRequestHandler<CreateMimboxCommand>
{
    private readonly IMimboxRepository _mimboxRepository;

    public CreateMimblyCommandHandler(IMimboxRepository mimboxRepository) => _mimboxRepository = mimboxRepository;

    public async Task<Unit> Handle(CreateMimboxCommand request, CancellationToken cancellationToken)
    {
        // Might not be required. Call this before calling the api.
        await request.CreateMimboxRequest.Validate();

        // This runs a single task. If several entities use Task.WhenAll
        await Task.Run(() => request.CreateMimboxRequest.Validate(), cancellationToken);

        return Unit.Value;
    }
}