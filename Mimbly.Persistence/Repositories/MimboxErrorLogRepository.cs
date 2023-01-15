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
