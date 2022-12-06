namespace Mimbly.Application.Queries.MimboxLocation.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class GetAllMimboxLocationHandler : IRequestHandler<GetAllMimboxLocationsQuery, AllMimboxLocationsVm>
{
    private readonly IMimboxLocationRepository _mimboxLocationRepository;
    private readonly IMapper _mapper;

    public GetAllMimboxLocationHandler(
        IMimboxLocationRepository mimboxLocationRepository,
        IMapper mapper)
    {
        _mimboxLocationRepository = mimboxLocationRepository;
        _mapper = mapper;
    }

    public async Task<AllMimboxLocationsVm> Handle(GetAllMimboxLocationsQuery request, CancellationToken cancellationToken)
    {
        var mimboxes = await _mimboxLocationRepository.GetAllMimboxLocations();

        var mimboxLocationDtos = _mapper.Map<IEnumerable<MimboxLocationDto>>(mimboxes);

        return new AllMimboxLocationsVm { MimboxLocations = mimboxLocationDtos };
    }
}