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
