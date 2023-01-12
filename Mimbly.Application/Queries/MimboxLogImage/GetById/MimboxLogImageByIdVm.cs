namespace Mimbly.Application.Queries.MimboxLogImage.GetById;

using Mimbly.Application.Contracts.Dtos.MimboxLogImage;

public class MimboxLogImageByIdVm
{
    public MimboxLogImageDto MimboxLogImage { get; set; }

    public MimboxLogImageByIdVm() => MimboxLogImage = new MimboxLogImageDto();
}