namespace Mimbly.Persistence.Repositories;

using System.Threading.Tasks;
using Dapper;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class MimboxLogImageRepository : IMimboxLogImageRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxLogImageRepository(
       ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<MimboxLogImage>> GetMimboxLogImagesByMimboxLogId(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Log_Image
            WHERE Mimbox_Log_Id = @id
        ";

        return await _db.LoadEntities<MimboxLogImage, dynamic>(sql, new { Id = id });
    }

    public async Task<MimboxLogImage> GetMimboxLogImageById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Log_Image
            WHERE Id = @id
        ";

        return await _db.LoadEntity<MimboxLogImage, dynamic>(sql, new { Id = id });
    }

    public async Task CreateMimboxLogImage(MimboxLogImage mimboxLogImage)
    {
        var sql =
        @"
            INSERT INTO Mimbox_Log_Image
                (Id, Url, Mimbox_Log_Id)
            VALUES
                (@Id, @Url, @MimboxLogId)
        ";

        await _db.SaveChanges(sql, mimboxLogImage);
    }

    public async Task DeleteMimboxLogImage(MimboxLogImage mimboxLogImage)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox_Log_Image
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxLogImage);
    }
}