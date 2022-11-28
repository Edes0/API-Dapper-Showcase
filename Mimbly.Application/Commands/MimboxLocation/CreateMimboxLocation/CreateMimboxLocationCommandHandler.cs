namespace Mimbly.Application.Commands.MimboxLocation.CreateMimboxLocation;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimboxLocationCommandHandler : IRequestHandler<CreateMimboxLocationCommand, MimboxLocation>
{
    private readonly IMimboxLocationRepository _mimboxLocationRepository;
    private readonly IMapper _mapper;

    public CreateMimboxLocationCommandHandler(
        IMimboxLocationRepository mimboxLocationRepository,
        IMapper mapper)
    {
        _mimboxLocationRepository = mimboxLocationRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLocation> Handle(CreateMimboxLocationCommand request, CancellationToken cancellationToken)
    {
        await request.CreateMimboxLocationRequest.Validate();

        var mimboxLocationEntity = _mapper.Map<MimboxLocation>(request.CreateMimboxLocationRequest);

        await _mimboxLocationRepository.CreateMimboxLocation(mimboxLocationEntity);

        return mimboxLocationEntity;
    }
}