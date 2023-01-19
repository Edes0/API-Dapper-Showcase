namespace Mimbly.Persistence.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class MimboxLocationRepository : IMimboxLocationRepository
{
    private readonly ISqlDataAccess _db;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public MimboxLocationRepository(
        ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <summary>
    /// Method <c>GetAllMimboxLocations</c> queries the database
    /// for all <c>MimboxLocation</c>.
    /// </summary>
    /// <returns>A <c>IEnumerable</c> of <c>MimboxLocation</c>.</returns>
    public async Task<IEnumerable<MimboxLocation>> GetAllMimboxLocations()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Location
        ";

        return await _db.LoadEntities<MimboxLocation, dynamic>(sql, new { });
    }

    /// <summary>
    /// Method <c>GetMimboxLocationById</c> queries the database
    /// for a single <c>MimboxLocation</c> using the provided <c>mimboxLocationId</c>
    /// </summary>
    /// <param name="id">The <c>mimboxLocationId</c> to look for.</param>
    /// <returns>A single <c>MimboxLocation</c>.</returns>
    public async Task<MimboxLocation> GetMimboxLocationById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Location
            WHERE Id = @id
        ";

        return await _db.LoadEntity<MimboxLocation, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>CreateMimboxLocation</c> inserts a single <c>MimboxLocation</c>
    /// into the database.
    /// </summary>
    /// <param name="location">The <c>MimboxLocation</c> to be inserted.</param>
    public async Task CreateMimboxLocation(MimboxLocation location)
    {
        var sql =
        @"
            INSERT INTO Mimbox_Location
                (Id, Country, Region, Postal_code, City, Street_address)
            VALUES
                (@Id, @Country, @Region, @PostalCode, @City, @StreetAddress)
        ";

        await _db.SaveChanges(sql, location);
    }

    /// <summary>
    /// Method <c>DeleteMimboxLocation</c> removes the provided
    /// <c>MimboxLocation</c> from the database.
    /// </summary>
    /// <param name="location">The <c>MimboxLocation</c> to be removed.</param>
    public async Task DeleteMimboxLocation(MimboxLocation location)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox_Location
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, location);
    }

    /// <summary>
    /// Method <c>UpdateMimboxLocation</c> updates the specified
    /// <c>MimboxLocation</c> in the database with new values.
    /// </summary>
    /// <param name="mimboxModel">The <c>MimboxLocation</c> to be updated.</param>
    public async Task UpdateMimboxLocation(MimboxLocation location)
    {
        var sql =
        @"
            UPDATE Mimbox_Location
            SET Country = @Country,
                Region = @Region,
                Postal_code = @PostalCode,
                City = @City,
                Street_address = @StreetAddress
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, location);
    }


}
