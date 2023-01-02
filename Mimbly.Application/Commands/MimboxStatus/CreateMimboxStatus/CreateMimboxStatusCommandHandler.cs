namespace Mimbly.Application.Commands.MimboxStatus.CreateMimboxStatus;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimboxStatusCommandHandler : IRequestHandler<CreateMimboxStatusCommand, MimboxStatus>
{
    private readonly IMimboxStatusRepository _mimboxStatusRepository;
    private readonly IMapper _mapper;

    public CreateMimboxStatusCommandHandler(
        IMimboxStatusRepository mimboxStatusRepository,
        IMapper mapper)
    {
        _mimboxStatusRepository = mimboxStatusRepository;
        _mapper = mapper;
    }

    public async Task<MimboxStatus> Handle(CreateMimboxStatusCommand request, CancellationToken cancellationToken)
    {
        await request.CreateMimboxStatusRequest.Validate();

        var mimboxStatusEntity = _mapper.Map<MimboxStatus>(request.CreateMimboxStatusRequest);

        await _mimboxStatusRepository.CreateMimboxStatus(mimboxStatusEntity);

        return mimboxStatusEntity;
    }
}