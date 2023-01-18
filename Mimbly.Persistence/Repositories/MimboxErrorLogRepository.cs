namespace Mimbly.Persistence.Repositories;

using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Dapper;
using Mimbly.Domain.Entities.AzureEvents;

public class MimboxErrorLogRepository : IMimboxErrorLogRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxErrorLogRepository(
        ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <summary>
    /// Method <c>GetErrorLogsByMimboxId</c> queries the database
    /// for a list of <c>MimboxErrorLog</c> using the provided <c>mimboxId</c>.
    /// </summary>
    /// <param name="id">The <c>mimboxId</c> to look for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>MimboxErrorLog</c>.</returns>
    public async Task<IEnumerable<MimboxErrorLog>> GetErrorLogsByMimboxId(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Error_Log
            WHERE Mimbox_Id = @id
        ";

        return await _db.LoadEntities<MimboxErrorLog, dynamic>(sql, new { id });
    }

    /// <summary>
    /// Method <c>GetErrorLogsByMimboxIds</c> queries the database
    /// for a list of <c>MimboxErrorLog</c> that belongs to different logs
    /// by using the provided list of <c>mimboxId</c>.
    /// </summary>
    /// <param name="ids">The list of <c>mimboxId</c> to query for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>MimboxErrorLog</c>.</returns>
    public async Task<IEnumerable<MimboxErrorLog>> GetErrorLogsByMimboxIds(IEnumerable<Guid> ids)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Error_Log
            WHERE Mimbox_Id IN @ids
        ";

        return await _db.LoadEntities<MimboxErrorLog, dynamic>(sql, new { ids });
    }

    /// <summary>
    /// Method <c>UpdateMimboxErrorLog</c> updates the specified
    /// <c>MimboxErrorLog</c> in the database with new values.
    /// </summary>
    /// <param name="mimboxErrorLog">The <c>MimboxErrorLog</c> to be updated.</param>
    public async Task UpdateMimboxErrorLog(MimboxErrorLog mimboxErrorLog)
    {
        var sql =
        @"
            UPDATE Mimbox_Error_Log
            SET Discarded = @Discarded
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxErrorLog);
    }
}
