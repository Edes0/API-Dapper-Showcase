namespace Mimbly.Persistence.Repositories;

using System.Data;
using System.Data.SqlClient;
using Application.Common.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;

public class MimblyRepository : IMimblyRepository
{
    private readonly IConfiguration _config;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public MimblyRepository(IConfiguration config)
    {
        _config = config;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Mimbly>> GetMimblys()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbly
        ";

        string ConnectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(ConnectionString);
        return await connection.QueryAsync<Mimbly>(sql);
    }

    public async Task<IEnumerable<Mimbly>> GetMimblysFilteredMinByAge(int age)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbly
            WHERE age >= @Age
        ";

        string ConnectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(ConnectionString);
        return await connection.QueryAsync<Mimbly>(
            sql, new
            {
                Age = age
            });
    }
}