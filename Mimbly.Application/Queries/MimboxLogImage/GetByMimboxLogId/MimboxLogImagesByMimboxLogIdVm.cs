namespace Mimbly.Application.Queries.MimboxLogImage.GetByMimboxLogId;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.MimboxLogImage;

public class MimboxLogImagesByMimboxLogIdVm
{
    public IEnumerable<MimboxLogImageDto> MimboxLogImages { get; set; }

    public MimboxLogImagesByMimboxLogIdVm() => MimboxLogImages = new List<MimboxLogImageDto>();
}
