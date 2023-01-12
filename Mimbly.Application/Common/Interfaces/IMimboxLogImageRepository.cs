namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxLogImageRepository
{
    Task CreateMimboxLogImage(MimboxLogImage mimboxLogImageList);
    Task DeleteMimboxLogImagesByMimboxLogId(MimboxLog mimboxLog);
    Task DeleteMimboxLogImage(MimboxLogImage mimboxLogImage);
    Task<IEnumerable<MimboxLogImage>> GetMimboxLogImagesByMimboxLogId(Guid id);
}