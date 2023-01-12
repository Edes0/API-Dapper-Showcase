namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxLogRepository
{
    Task CreateMimboxLog(MimboxLog mimboxLog);
    Task DeleteMimboxLog(MimboxLog mimboxLog);
    Task<IEnumerable<MimboxLog>> GetMimboxLogsByMimboxId(Guid id);
    Task<MimboxLog> GetMimboxLogById(Guid id);
    Task UpdateMimboxLog(MimboxLog mimboxLog);
}