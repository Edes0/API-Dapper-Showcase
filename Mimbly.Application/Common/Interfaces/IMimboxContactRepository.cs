namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxContactRepository
{
    Task CreateMimboxContact(MimboxContact mimboxContact);
    Task DeleteMimboxContact(MimboxContact mimboxContact);
    Task<IEnumerable<MimboxContact>> GetAllMimboxContacts();
    Task<MimboxContact> GetMimboxContactById(Guid id);
    Task<IEnumerable<MimboxContact>> GetMimboxContactsByMimboxId(Guid id);
    Task UpdateMimboxContact(MimboxContact mimboxContact);
}