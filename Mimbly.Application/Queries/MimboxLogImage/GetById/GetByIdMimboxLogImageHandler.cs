namespace Mimbly.Application.Queries.MimboxLogImage.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxLogImage;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxLogImageHandler : IRequestHandler<GetByIdMimboxLogImageQuery, MimboxLogImageByIdVm>
{
    private readonly IMimboxLogImageRepository _mimboxLogImageRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxLogImageHandler(
        IMimboxLogImageRepository mimboxLogImageRepository,
        IMapper mapper)
    {
        _mimboxLogImageRepository = mimboxLogImageRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLogImageByIdVm> Handle(GetByIdMimboxLogImageQuery request, CancellationToken cancellationToken)
    {
        var mimboxLogImage = await _mimboxLogImageRepository.GetMimboxLogImageById(request.Id);

        if (mimboxLogImage == null)
            throw new NotFoundException($"Can't find mimbox log image with id: {request.Id}");

        var mimboxLogImageDto = _mapper.Map<MimboxLogImageDto>(mimboxLogImage);

        return new MimboxLogImageByIdVm { MimboxLogImage = mimboxLogImageDto };
    }
}