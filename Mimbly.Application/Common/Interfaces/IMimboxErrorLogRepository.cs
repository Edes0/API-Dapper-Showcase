namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities.AzureEvents;

public interface IMimboxErrorLogRepository
{
    Task<IEnumerable<MimboxErrorLog>> GetErrorLogsByMimboxId(Guid id);
    Task UpdateMimboxErrorLog(MimboxErrorLog mimboxErrorLog);
}