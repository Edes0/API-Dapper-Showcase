namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxRepository
{
    Task<IEnumerable<Mimbox>> GetAllMimboxes();
    Task<IEnumerable<Mimbox>> GetMimboxById(Guid id);
    Task CreateMimbox(Mimbox mimbox);
    Task DeleteMimbox(Mimbox mimbox);
}