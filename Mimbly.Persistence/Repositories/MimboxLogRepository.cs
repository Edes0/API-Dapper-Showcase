namespace Mimbly.Persistence.Repositories;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class MimboxLogRepository : IMimboxLogRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxLogRepository(
        ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <summary>
    /// Method <c>GetMimboxLogById</c> queries the database
    /// for a single <c>mimboxLog</c>.
    /// </summary>
    /// <param name="id">The <c>mimboxLog</c> to look for.</param>
    /// <returns></returns>
    public async Task<MimboxLog> GetMimboxLogById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Log
            WHERE Id = @id
        ";

        return await _db.LoadEntity<MimboxLog, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>GetMimboxLogByMimboxId</c> queries the database
    /// for a list of <c>mimboxLog</c> using the provided <c>mimboxId</c>
    /// </summary>
    /// <param name="id">The <c>mimboxId</c> to look for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>mimboxLog</c>.</returns>
    public async Task<IEnumerable<MimboxLog>> GetMimboxLogsByMimboxId(Guid id)
    {
        var sql =
        @"
            SELECT ml.*
            FROM Mimbox_Log ml
            WHERE ml.Mimbox_Id = @id
        ";

        return await _db.LoadEntities<MimboxLog, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>CreateMimboxLog</c> inserts a single
    /// <c>mimboxLog</c> into the database.
    /// </summary>
    /// <param name="mimboxLog">The <c>mimboxLog</c> to be inserted.</param>
    public async Task CreateMimboxLog(MimboxLog mimboxLog)
    {
        var sql =
        @"
            INSERT INTO Mimbox_Log
                (Id, Log, Created_At, Mimbox_Id)
            VALUES
                (@Id, @Log, @CreatedAt, @MimboxId)
        ";

        await _db.SaveChanges(sql, mimboxLog);
    }

    /// <summary>
    /// Method <c>DeleteMimboxLog</c> removes the provided
    /// <c>mimboxLog</c> from the database.
    /// </summary>
    /// <param name="mimboxLog">The <c>mimboxLog</c> to be removed.</param>
    public async Task DeleteMimboxLog(MimboxLog mimboxLog)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox_Log
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxLog);
    }

    /// <summary>
    /// Method <c>UpdateMimboxModel</c> updates the specified
    /// <c>mimboxLog</c> in the database with new values.
    /// </summary>
    /// <param name="mimboxLog">The <c>mimboxLog</c> to be updated.</param>
    public async Task UpdateMimboxLog(MimboxLog mimboxLog)
    {
        var sql =
        @"
            UPDATE Mimbox_Log
            SET Log = @Log
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxLog);
    }
}
