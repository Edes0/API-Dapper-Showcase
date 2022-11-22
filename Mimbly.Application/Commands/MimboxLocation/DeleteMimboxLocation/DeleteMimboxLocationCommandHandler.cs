namespace Mimbly.Application.Commands.MimboxLocation.DeleteMimboxLocation;

using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteMimboxLocationCommandHandler : IRequestHandler<DeleteMimboxLocationCommand>
{
    private readonly IMimboxLocationRepository _mimboxLocationRepository;

    public DeleteMimboxLocationCommandHandler(
        IMimboxLocationRepository mimboxLocationRepository)
    {
        _mimboxLocationRepository = mimboxLocationRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxLocationCommand request, CancellationToken cancellationToken)
    {
        var mimboxLocation = await _mimboxLocationRepository.GetMimboxLocationById(request.Id);

        if (mimboxLocation == null)
            throw new NotFoundException($"Can't find mimbox location with id: {request.Id}");

        await _mimboxLocationRepository.DeleteMimboxLocation(mimboxLocation);

        return Unit.Value;
    }
}