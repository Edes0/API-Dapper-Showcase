namespace Mimbly.Persistence.Repositories;

using System;
using Application.Common.Interfaces;
using Dapper;
using Domain.Enitites;

public class MimboxRepository : IMimboxRepository
{
    private readonly ISqlDataAccess _db;
    public string ConnectionStringName { get; set; } = "DbConnectionString";


    public MimboxRepository(ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Mimbox>> GetAllMimboxes()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox
        ";

        return await _db.LoadData<Mimbox, dynamic>(sql, new { });
    }

    public async Task<IEnumerable<Mimbox>> GetMimboxById(Guid Id) //TODO: Change db, then change this
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox
            WHERE id = @Id
        ";

        return await _db.LoadData<Mimbox, dynamic>(sql, new { id = Id });
    }

    public async Task<IEnumerable<Mimbox>> GetMimboxesFilteredMinByAge(int Age) //TODO: Change db, then change this
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox
            WHERE age >= @Age
        ";

        return await _db.LoadData<Mimbox, dynamic>(sql, new { age = Age });
    }

    public async Task CreateMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            INSERT INTO Mimbox
                (id, first_name, last_name, age)
            VALUES
                (@Id, @FirstName, @LastName, @Age)
        ";

        await _db.SaveData(sql, mimbox);
    }

    public async Task DeleteMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox
            WHERE id = @Id
        ";

        await _db.SaveData(sql, mimbox);
    }
}