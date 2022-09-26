namespace Boilerplate.Persistence.Repositories;

using Application.Common.Interfaces;
using Application.Common.ServiceOptions;
using Dapper;
using Domain.Models;
using MySql.Data.MySqlClient;

public class BoilerplateRepository : IBoilerplateRepository
{
    private readonly ConnectionStrings _connectionOptions;

    public BoilerplateRepository(ConnectionStrings connectionOptions)
    {
        _connectionOptions = connectionOptions;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Boilerplate>> GetBoilerPlates()
    {
        const string sqlQueryString =
        @"
            SELECT *
            FROM boilerplate
        ";

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        return await dbConnection.QueryAsync<Boilerplate>(
            sqlQueryString);
    }

    public async Task<IEnumerable<Boilerplate>> GetBoilerPlatesFilteredMinByAge(int age)
    {
        const string sqlQueryString =
        @"
            SELECT *
            FROM boilerplate
            WHERE age >= @Age
        ";

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        return await dbConnection.QueryAsync<Boilerplate>(
            sqlQueryString, new
            {
                Age = age
            });
    }
}