namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEventLogRepository
{
    Task<IEnumerable<string>> GetEventLogByMimboxId(Guid id);
    Task<IEnumerable<string>> GetTop10EventLogByMimboxId(Guid id);
    Task<IEnumerable<string>> GetEventLogBetweenDatesByMimboxId(Guid id, DateTime startDate, DateTime endDate);
}