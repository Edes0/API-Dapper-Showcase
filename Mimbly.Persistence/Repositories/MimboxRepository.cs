namespace Mimbly.Persistence.Repositories;
using Application.Common.Interfaces;
using Domain.Enitites;

public class MimboxRepository : IMimboxRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxRepository(ISqlDataAccess db) => _db = db;

    public async Task<IEnumerable<Mimbox>> GetMimblys()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbly
        ";

        return await _db.LoadData<Mimbox, dynamic>(sql, new { });
    }

    public async Task<IEnumerable<Mimbox>> GetMimblysFilteredMinByAge(int age)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbly
            WHERE age >= @Age
        ";

        return await _db.LoadData<Mimbox, dynamic>(sql,
            new
            {
                Age = age
            });
    }
}