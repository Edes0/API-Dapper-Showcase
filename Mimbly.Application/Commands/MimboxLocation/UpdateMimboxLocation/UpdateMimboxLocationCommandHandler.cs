namespace Mimbly.Application.Commands.MimboxLocation.UpdateMimboxLocation;

using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateMimboxLocationCommandHandler : IRequestHandler<UpdateMimboxLocationCommand>
{
    private readonly IMimboxLocationRepository _mimboxLocationRepository;
    private readonly IMapper _mapper;

    public UpdateMimboxLocationCommandHandler(
        IMimboxLocationRepository mimboxLocationRepository,
        IMapper mapper)
    {
        _mimboxLocationRepository = mimboxLocationRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxLocationCommand request, CancellationToken cancellationToken)
    {
        await request.UpdateMimboxLocationRequest.Validate();

        var mimboxLocationEntity = _mapper.Map<MimboxLocation>(request.UpdateMimboxLocationRequest);

        mimboxLocationEntity.Id = request.Id;

        await _mimboxLocationRepository.UpdateMimboxLocation(mimboxLocationEntity);

        return Unit.Value;
    }
}
