namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities.AzureEvents;

public interface IMimboxErrorLogRepository
{
    Task<IEnumerable<MimboxErrorLog>> GetErrorLogsByMimboxId(Guid id);
    Task<IEnumerable<MimboxErrorLog>> GetErrorLogsByMimboxIds(IEnumerable<Guid> ids);
    Task UpdateMimboxErrorLog(MimboxErrorLog mimboxErrorLog);
}