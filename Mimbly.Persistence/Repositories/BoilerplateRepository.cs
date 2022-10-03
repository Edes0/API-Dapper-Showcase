namespace Mimbly.Persistence.Repositories;

using Application.Common.Interfaces;
using Application.Common.ServiceOptions;
using Dapper;
using Domain.Models;
using MySql.Data.MySqlClient;

public class MimblyRepository : IMimblyRepository
{
    private readonly ConnectionStrings _connectionOptions;

    public MimblyRepository(ConnectionStrings connectionOptions)
    {
        _connectionOptions = connectionOptions;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Mimbly>> GetMimblys()
    {
        const string sqlQueryString =
        @"
            SELECT *
            FROM Mimbly
        ";

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        return await dbConnection.QueryAsync<Mimbly>(
            sqlQueryString);
    }

    public async Task<IEnumerable<Mimbly>> GetMimblysFilteredMinByAge(int age)
    {
        const string sqlQueryString =
        @"
            SELECT *
            FROM Mimbly
            WHERE age >= @Age
        ";

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        return await dbConnection.QueryAsync<Mimbly>(
            sqlQueryString, new
            {
                Age = age
            });
    }
}