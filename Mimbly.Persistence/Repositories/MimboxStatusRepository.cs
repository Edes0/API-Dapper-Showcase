namespace Mimbly.Persistence.Repositories;

using System.Threading.Tasks;
using Dapper;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class MimboxStatusRepository : IMimboxStatusRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxStatusRepository(
       ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <summary>
    /// Method <c>GetAllMimboxes</c> queries the database
    /// for every <c>mimboxStatus</c>.
    /// </summary>
    /// <returns>A <c>IEnumerable</c> of <c>mimboxStatus</c>.</returns>
    public async Task<IEnumerable<MimboxStatus>> GetAllMimboxStatuses()
    {
        var sql =
        @"
        SELECT *
        FROM Mimbox_Status
        ";

        return await _db.LoadEntities<MimboxStatus, dynamic>(sql, new { });
    }

    /// <summary>
    /// Method <c>GetMimboxStatusById</c> queries the database
    /// for a single <c>mimboxStatus</c>.
    /// </summary>
    /// <param name="id">The <c>statusId</c> to look for.</param>
    /// <returns>A single <c>mimboxStatus</c>.</returns>
    public async Task<MimboxStatus> GetMimboxStatusById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Status
            WHERE Id = @id
        ";

        return await _db.LoadEntity<MimboxStatus, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>CreateMimboxStatus</c> inserts a single <c>mimboxStatus</c>
    /// into the database.
    /// </summary>
    /// <param name="mimboxStatus">The <c>mimboxStatus</c> to be inserted.</param>
    public async Task CreateMimboxStatus(MimboxStatus mimboxStatus)
    {
        var sql =
        @"
            INSERT INTO Mimbox_Status
                (Id, Name)
            VALUES
                (@Id, @Name)
        ";

        await _db.SaveChanges(sql, mimboxStatus);
    }

    /// <summary>
    /// Method <c>DeleteMimboxStatus</c> removes the provided
    /// <c>mimboxStatus</c> from the database.
    /// </summary>
    /// <param name="mimboxStatus">The mimboxStatus to be removed.</param>
    public async Task DeleteMimboxStatus(MimboxStatus mimboxStatus)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox_Status
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxStatus);
    }

    /// <summary>
    /// Method <c>UpdateMimboxStatus</c> updates the specified
    /// <c>mimboxStatus</c> in the database with new values.
    /// </summary>
    /// <param name="mimboxStatus">The <c>mimboxStatus</c> to be updated.</param>
    public async Task UpdateMimboxStatus(MimboxStatus mimboxStatus)
    {
        var sql =
        @"
            UPDATE Mimbox_Status
            SET Name = @Name
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxStatus);
    }
}
