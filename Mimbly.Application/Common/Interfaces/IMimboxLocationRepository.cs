namespace Mimbly.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimbly.Domain.Entities;

public interface IMimboxLocationRepository
{
    string ConnectionStringName { get; set; }

    Task CreateMimboxLocation(MimboxLocation location);
    Task DeleteMimboxLocation(MimboxLocation location);
    Task<IEnumerable<MimboxLocation>> GetAllMimboxLocations();
    Task<MimboxLocation> GetMimboxLocationById(Guid id);
    Task UpdateMimboxLocation(MimboxLocation location);
}