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

    public async Task<IEnumerable<Mimbox>> GetMimboxById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox
            WHERE Id = @id
        ";

        return await _db.LoadData<Mimbox, dynamic>(sql, new { });
    }

    public async Task<IEnumerable<Mimbox>> GetMimboxesFilteredMinByAge(int age)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox
            WHERE age >= @Age
        ";

        return await _db.LoadData<Mimbox, dynamic>(sql,
            new
            {
                Age = age
            });
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

    public async Task DeleteMimbox(Guid id)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox
            WHERE Id = @id
        ";

        await _db.SaveData(sql, id);
    }
}