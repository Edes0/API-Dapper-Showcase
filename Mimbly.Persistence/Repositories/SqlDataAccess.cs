namespace Mimbly.Persistence.Repositories;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Mimbly.Application.Common.Interfaces;

public class SqlDataAccess : ISqlDataAccess
{

    private readonly IConfiguration _config;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public SqlDataAccess(IConfiguration config) => _config = config;


    public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);
        var data = await connection.QueryAsync<T>(sql, parameters);

        return data.ToList();
    }

    public async Task SaveData<T>(string sql, T parameters)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task Transaction(params string[] sqlArray)
    {
        try
        {
            var connectionString = _config.GetConnectionString(ConnectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();

            foreach (var sql in sqlArray)
            {
                await connection.ExecuteAsync(sql, transaction);
            }
            transaction.Commit();
            //connection.Close();
        }
        catch (Exception)
        {
            throw new NotImplementedException(); //TODO: FIX: TransactionFailedException();
        }
    }

    public async Task SaveDataQuery(string sql) //TODO: Remove this before prod
    {
        string ConnectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(ConnectionString);
        await connection.QueryAsync(sql);
    }

    public async Task<T> LoadOneObject<T, U>(string sql, U parameter) //TODO: Remove this before prod
    {
        string ConnectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(ConnectionString);
        var data = await connection.QueryAsync<T>(sql, parameter);

        return data.FirstOrDefault();
    }
}