namespace Mimbly.Application.Queries.MimboxLogImage.GetByMimboxLogId;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxLogImage;
using Mimbly.CoreServices.Exceptions;

public class GetByMimboxLogIdMimboxLogImageHandler : IRequestHandler<GetByMimboxLogIdMimboxLogImageQuery, MimboxLogImagesByMimboxLogIdVm>
{
    private readonly IMimboxLogImageRepository _mimboxLogImageRepository;
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly IMapper _mapper;

    public GetByMimboxLogIdMimboxLogImageHandler(
        IMimboxLogImageRepository mimboxLogImageRepository,
        IMimboxLogRepository mimboxLogRepository,
        IMapper mapper)
    {
        _mimboxLogImageRepository = mimboxLogImageRepository;
        _mimboxLogRepository = mimboxLogRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLogImagesByMimboxLogIdVm> Handle(GetByMimboxLogIdMimboxLogImageQuery request, CancellationToken cancellationToken)
    {
        var mimboxLog = await _mimboxLogRepository.GetMimboxLogById(request.Id);

        if (mimboxLog == null)
            throw new NotFoundException($"Can't find mimbox log with id: {request.Id}");

        var mimboxLogImages = await _mimboxLogImageRepository.GetMimboxLogImagesByMimboxLogId(mimboxLog.Id);

        var mimboxLogImageDtos = _mapper.Map<IEnumerable<MimboxLogImageDto>>(mimboxLogImages);

        return new MimboxLogImagesByMimboxLogIdVm { MimboxLogImages = mimboxLogImageDtos };
    }
}