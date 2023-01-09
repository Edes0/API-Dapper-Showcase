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

    public async Task<IEnumerable<MimboxStatus>> GetAllMimboxStatuses()
    {
        var sql =
        @"
        SELECT *
        FROM Mimbox_Status
        ";

        return await _db.LoadEntities<MimboxStatus, dynamic>(sql, new { });
    }

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
