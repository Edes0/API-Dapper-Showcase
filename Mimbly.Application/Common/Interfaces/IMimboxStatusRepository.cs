namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxStatusRepository
{
    Task CreateMimboxStatus(MimboxStatus mimboxStatus);
    Task DeleteMimboxStatus(MimboxStatus mimboxStatus);
    Task<IEnumerable<MimboxStatus>> GetAllMimboxStatuses();
    Task<MimboxStatus> GetMimboxStatusById(Guid id);
    Task UpdateMimboxStatus(MimboxStatus mimboxStatus);
}