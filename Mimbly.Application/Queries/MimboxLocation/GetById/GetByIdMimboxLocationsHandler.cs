namespace Mimbly.Application.Queries.MimboxLocation.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxLocationHandler : IRequestHandler<GetByIdMimboxLocationQuery, MimboxLocationByIdVm>
{
    private readonly IMimboxLocationRepository _mimboxLocationRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxLocationHandler(
        IMimboxLocationRepository mimboxLocationRepository,
        IMapper mapper)
    {
        _mimboxLocationRepository = mimboxLocationRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLocationByIdVm> Handle(GetByIdMimboxLocationQuery request, CancellationToken cancellationToken)
    {
        var mimboxLocation = await _mimboxLocationRepository.GetMimboxLocationById(request.Id);

        if (mimboxLocation == null)
            throw new NotFoundException($"Can't find mimbox location with id: {request.Id}");

        var mimboxLocationDto = _mapper.Map<MimboxLocationDto>(mimboxLocation);

        return new MimboxLocationByIdVm { MimboxLocation = mimboxLocationDto };
    }
}