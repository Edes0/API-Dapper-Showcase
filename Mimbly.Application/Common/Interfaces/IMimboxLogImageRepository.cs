namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxLogImageRepository
{
    Task CreateMimboxLogImage(MimboxLogImage mimboxLogImage);
    Task DeleteMimboxLogImage(MimboxLogImage mimboxLogImage);
    Task<IEnumerable<MimboxLogImage>> GetMimboxLogImagesByMimboxLogId(Guid id);
    Task<MimboxLogImage> GetMimboxLogImageById(Guid id);
    Task<IEnumerable<MimboxLogImage>> GetMimboxLogImagesByMimboxLogIds(IEnumerable<Guid> ids);
}