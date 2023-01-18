namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxRepository
{
    Task CreateMimbox(Mimbox mimbox);
    Task DeleteMimbox(Mimbox mimbox);
    Task UpdateMimbox(Mimbox mimbox);
    Task<IEnumerable<Mimbox>> GetAllMimboxes();
    Task<Mimbox> GetMimboxById(Guid id);
    Task<IEnumerable<Mimbox>> GetMimboxesByCompanyIds(IEnumerable<Guid> ids);
    Task<IEnumerable<Mimbox>> GetMimboxesByCompanyId(Guid id);
}