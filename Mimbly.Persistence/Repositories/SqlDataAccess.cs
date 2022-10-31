namespace Mimbly.Persistence.Repositories;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Mimbly.Application.Common.Interfaces;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public SqlDataAccess(IConfiguration config) => _config = config;

    public async Task<IEnumerable<T>> LoadEntities<T, U>(string sql, U parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        var data = await connection.QueryAsync<T>(sql, parameters);

        return data;
    }

    public async Task<T> LoadEntity<T, U>(string sql, U parameter)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameter);

        return data;
    }

    public async Task SaveChanges<T>(string sql, T parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteScalarAsync<int>(sql, parameters);
    }

    public async Task Transaction(params string[] sqlArray)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        var transaction = await connection.BeginTransactionAsync();

        foreach (var sql in sqlArray)
        {
            await connection.ExecuteAsync(sql, transaction);
        }

        await transaction.CommitAsync();
    }
}