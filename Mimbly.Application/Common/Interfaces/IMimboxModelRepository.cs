namespace Mimbly.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxModelRepository
{
    Task CreateMimboxModel(MimboxModel mimboxModel);
    Task DeleteMimboxModel(MimboxModel mimboxModel);
    Task<IEnumerable<MimboxModel>> GetAllMimboxModels();
    Task<MimboxModel> GetMimboxModelById(Guid id);
    Task UpdateMimboxModel(MimboxModel mimboxModel);
}