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

    /// <summary>
    /// Method <c>GetMimboxLogImagesByMimboxLogId</c> queries the database
    /// for a single <c>mimboxLogImage</c> using the provided <c>mimboxLogId</c>
    /// </summary>
    /// <param name="id">The <c>mimboxLogId</c> to look for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>mimboxLogImage</c>.</returns>
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
    /// <summary>
    /// Method <c>GetMimboxLogImageById</c> queries the database
    /// for a single <c>MimboxLogImage</c> using the provided <c>modelId</c>
    /// </summary>
    /// <param name="id">The <c>MimboxLogImageId</c> to look for.</param>
    /// <returns>A single <c>MimboxLogImage</c>.</returns>
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

    /// <summary>
    /// Method <c>CreateMimboxLogImage</c> inserts a single <c>MimboxLogImage</c>
    /// into the database.
    /// </summary>
    /// <param name="mimboxLogImage">The <c>MimboxLogImage</c> to be inserted.</param>
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

    /// <summary>
    /// Method <c>DeleteMimboxLogImage</c> removes the provided
    /// <c>MimboxLogImage</c> from the database.
    /// </summary>
    /// <param name="mimboxLogImage">The <c>MimboxLogImage</c> to be removed.</param>
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

    /// <summary>
    /// Method <c>GetMimboxLogImagesByMimboxLogIds</c> queries the database
    /// for a list of <c>MimboxLogImage</c> that belongs to different logs
    /// by using the provided list of <c>mimboxLogId</c>.
    /// </summary>
    /// <param name="ids">The list of <c>mimboxLogId</c> to query for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>mimboxLogImage</c>.</returns>
    public async Task<IEnumerable<MimboxLogImage>> GetMimboxLogImagesByMimboxLogIds(IEnumerable<Guid> ids)
    {
        var sql =
        @"
            SELECT mli.*
            FROM Mimbox_Log_Image mli
            WHERE Mimbox_Log_Id IN @ids
        ";

        return await _db.LoadEntities<MimboxLogImage, dynamic>(sql, new { ids });
    }
}