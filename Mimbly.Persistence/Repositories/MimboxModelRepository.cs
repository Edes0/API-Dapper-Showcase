namespace Mimbly.Persistence.Repositories;

using System.Threading.Tasks;
using Dapper;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class MimboxModelRepository : IMimboxModelRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxModelRepository(
       ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <summary>
    /// Method <c>GetAllMimboxModels</c> queries the database
    /// for all <c>mimboxModel</c>.
    /// </summary>
    /// <returns>A <c>IEnumerable</c> of <c>mimboxModel</c>.</returns>
    public async Task<IEnumerable<MimboxModel>> GetAllMimboxModels()
    {
        var sql =
        @"
        SELECT *
        FROM Mimbox_Model
        ";

        return await _db.LoadEntities<MimboxModel, dynamic>(sql, new { });
    }

    /// <summary>
    /// Method <c>GetMimboxModelById</c> queries the database
    /// for a single <c>mimboxModel</c> using the provided <c>modelId</c>
    /// </summary>
    /// <param name="id">The <c>modelId</c> to look for.</param>
    /// <returns>A single <c>mimboxModel</c>.</returns>
    public async Task<MimboxModel> GetMimboxModelById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Model
            WHERE Id = @id
        ";

        return await _db.LoadEntity<MimboxModel, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>CreateMimboxModel</c> inserts a single <c>mimboxModel</c>
    /// into the database.
    /// </summary>
    /// <param name="mimboxModel">The <c>mimboxModel</c> to be inserted.</param>
    public async Task CreateMimboxModel(MimboxModel mimboxModel)
    {
        var sql =
        @"
            INSERT INTO Mimbox_Model
                (Id, Name)
            VALUES
                (@Id, @Name)
        ";

        await _db.SaveChanges(sql, mimboxModel);
    }

    /// <summary>
    /// Method <c>DeleteMimboxModel</c> removes the provided
    /// <c>mimboxModel</c> from the database.
    /// </summary>
    /// <param name="mimboxModel">The <c>mimboxModel</c> to be removed.</param>
    public async Task DeleteMimboxModel(MimboxModel mimboxModel)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox_Model
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxModel);
    }

    /// <summary>
    /// Method <c>UpdateMimboxModel</c> updates the specified
    /// <c>mimboxModel</c> in the database with new values.
    /// </summary>
    /// <param name="mimboxModel">The <c>mimboxModel</c> to be updated.</param>
    public async Task UpdateMimboxModel(MimboxModel mimboxModel)
    {
        var sql =
        @"
            UPDATE Mimbox_Model
            SET Name = @Name
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxModel);
    }
}
