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

    /// <summary>
    /// Method <c>LoadEntities</c> queries the database using
    /// the provided SQL query and returns a list of objects.
    /// </summary>
    /// <param name="sql">A string containing the SQL statement</param>
    /// <param name="parameters">Parameters used in the SQL statement</param>
    /// <typeparam name="T">Return type</typeparam>
    /// <typeparam name="U">SQL statement parameters type</typeparam>
    /// <returns>A list of specified object</returns>
    public async Task<IEnumerable<T>> LoadEntities<T, U>(string sql, U parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        var data = await connection.QueryAsync<T>(sql, parameters);

        return data;
    }

    /// <summary>
    /// Method <c>LoadEntity</c> queries the database using
    /// the provided SQL query and returns a single object.
    /// </summary>
    /// <param name="sql">A string containing the SQL statement</param>
    /// <param name="parameter">Parameters used in the SQL statement</param>
    /// <typeparam name="T">Return type</typeparam>
    /// <typeparam name="U">SQL statement parameter type</typeparam>
    /// <returns>A single specified object</returns>
    public async Task<T> LoadEntity<T, U>(string sql, U parameter)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameter);

        return data;
    }

    /// <summary>
    /// Method <c>SaveChanges</c> executes the provided SQL statement.
    /// </summary>
    /// <param name="sql">A string containing the SQL statement</param>
    /// <param name="parameters">Parameters used in the SQL statement</param>
    /// <typeparam name="T">SQL statement parameter type</typeparam>
    public async Task SaveChanges<T>(string sql, T parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql, parameters);
    }

    /// <summary>
    /// Method <c>Transaction</c> executes a transaction
    /// for the specified SQL statement.
    /// </summary>
    /// <param name="sql">A string containing the SQL statement</param>
    /// <param name="objectArray">A list of objects used in the SQL statement</param>
    /// <typeparam name="T">SQL statement parameters type</typeparam>
    public async Task Transaction<T>(string sql, List<T> objectArray)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        var transaction = await connection.BeginTransactionAsync();

        foreach (var part in objectArray)
        {
            await connection.ExecuteAsync(sql, part);
        }

        await transaction.CommitAsync();
    }
}