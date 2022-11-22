namespace Mimbly.Persistence.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class MimboxLocationRepository
{
    private readonly ISqlDataAccess _db;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public MimboxLocationRepository(
        ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<MimboxLocation>> GetAllLocations()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Location
        ";

        return await _db.LoadEntities<MimboxLocation, dynamic>(sql, new { });
    }

    public async Task<MimboxLocation> GetLocationById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Location
            WHERE Id = @id
        ";

        return await _db.LoadEntity<MimboxLocation, dynamic>(sql, new { Id = id });
    }

    public async Task CreateLocation(MimboxLocation location)
    {
        var sql =
        @"
            INSERT INTO Mimbox_Location
                (Id, Country, Region, Postal_code, City, Street_Address)
            VALUES
                (@Id, @Country, @Region, @PostalCode, @Cíty, @StreetAddress)
        ";

        await _db.SaveChanges(sql, location);
    }

    public async Task DeleteLocation(MimboxLocation location)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox_Location
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, location);
    }

    public async Task UpdateLocation(MimboxLocation location)
    {
        var sql =
        @"
            UPDATE Mimbox_Location
            SET Country = @Country,
                Region = @Region,
                Postal_code = @PostalCode,
                City = @City,
                Street_Address = @StreetAddress
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, location);
    }


}
