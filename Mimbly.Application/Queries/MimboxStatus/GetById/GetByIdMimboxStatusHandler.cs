namespace Mimbly.Application.Queries.MimboxStatus.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxStatusHandler : IRequestHandler<GetByIdMimboxStatusQuery, MimboxStatusByIdVm>
{
    private readonly IMimboxStatusRepository _mimboxStatusRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxStatusHandler(
        IMimboxStatusRepository mimboxStatusRepository,
        IMapper mapper)
    {
        _mimboxStatusRepository = mimboxStatusRepository;
        _mapper = mapper;
    }

    public async Task<MimboxStatusByIdVm> Handle(GetByIdMimboxStatusQuery request, CancellationToken cancellationToken)
    {
        var mimboxStatus = await _mimboxStatusRepository.GetMimboxStatusById(request.Id);

        if (mimboxStatus == null)
            throw new NotFoundException($"Can't find status with id: {request.Id}");

        var mimboxStatusDto = _mapper.Map<MimboxStatusDto>(mimboxStatus);

        return new MimboxStatusByIdVm { MimboxStatus = mimboxStatusDto };
    }
}