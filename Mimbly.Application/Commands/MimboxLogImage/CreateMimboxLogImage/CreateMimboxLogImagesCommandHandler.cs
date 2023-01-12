namespace Mimbly.Application.Commands.MimboxLogImageImage.CreateMimboxLogImageImage;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimboxLogImagesCommandHandler : IRequestHandler<CreateMimboxLogImageCommand, MimboxLogImage>
{
    private readonly IMimboxLogImageRepository _mimboxLogImageRepository;
    private readonly IMapper _mapper;

    public CreateMimboxLogImagesCommandHandler(
        IMimboxLogImageRepository mimboxLogImageRepository,
        IMapper mapper)
    {
        _mimboxLogImageRepository = mimboxLogImageRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLogImage> Handle(CreateMimboxLogImageCommand request, CancellationToken cancellationToken)
    {
        var mimboxLogImageEntity = _mapper.Map<MimboxLogImage>(request.CreateMimboxLogImageRequest);

        await _mimboxLogImageRepository.CreateMimboxLogImage(mimboxLogImageEntity);

        return mimboxLogImageEntity;
    }
}