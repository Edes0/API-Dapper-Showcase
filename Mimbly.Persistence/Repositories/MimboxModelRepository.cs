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

    public async Task<IEnumerable<MimboxModel>> GetAllMimboxModels()
    {
        var sql =
        @"
        SELECT *
        FROM Mimbox_Model
        ";

        return await _db.LoadEntities<MimboxModel, dynamic>(sql, new { });
    }

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
