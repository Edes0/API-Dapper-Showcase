namespace Mimbly.Persistence.Repositories;

using Application.Common.Interfaces;
using Dapper;
using Domain.Enitites;
using Microsoft.Extensions.Configuration;

public class MimboxRepository : IMimboxRepository
{
    private readonly ISqlDataAccess _db;

    private readonly IConfiguration _config;
    public string ConnectionStringName { get; set; } = "DbConnectionString";


    public MimboxRepository(ISqlDataAccess db, IConfiguration config)
    {
        _db = db;
        _config = config;
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

    public async Task<IEnumerable<Mimbox>> GetMimblysFilteredMinByAge(int age)
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
}