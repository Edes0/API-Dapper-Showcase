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
    private readonly IConfiguration _config;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public MimboxLogRepository(
        ISqlDataAccess db,
        IConfiguration config)
    {
        _db = db;
        _config = config;
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
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT ml.*, mli.*
            FROM Mimbox_Log ml
            LEFT JOIN Mimbox_Log_Image mli ON ml.Id = mli.Mimbox_Log_Id
            WHERE ml.Mimbox_Id = @id
        ";

        var lookup = new Dictionary<Guid, MimboxLog>();

        await connection.QueryAsync<MimboxLog, MimboxLogImage, MimboxLog>
           (sql, (mimboxLog, mimboxLogImage) =>
           {
               if (mimboxLog != null)
               {
                   if (!lookup.TryGetValue(mimboxLog.Id, out var mimboxLogRef))
                       lookup.Add(mimboxLog.Id, mimboxLogRef = mimboxLog);

                   if (mimboxLogImage != null)
                       mimboxLogRef.ImageList.Add(mimboxLogImage);
               }
               return null;
           },
           new { id });

        return lookup.Values;
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
