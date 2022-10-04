namespace Mimbly.Persistence.Repositories;

using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using Application.Common.Interfaces;
using Dapper;
using Domain.DomainModels;
using Microsoft.Extensions.Configuration;

public class IdentityRepository : IIdentityRepository
{
    private readonly IConfiguration _config;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public IdentityRepository(IConfiguration config)
    {
        _config = config;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Claim>> GetUserClaims(Guid userId)
    {
        await Task.Run(() => Console.WriteLine("Fetch claims from database here"));
        // Fetch user claims from database here if necessary. Use them when generating json web token in token handler.
        return new List<Claim>();
    }

    public async Task RegisterUser(string email, string password, string firstName, string lastName)
    {
        var sql =
        @"
            INSERT INTO user
                (id, email, password, first_name, last_name)
            VALUES
                (@Id, @Email, @Password, @FirstName, @LastName)
        ";

        var id = Guid.NewGuid();

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteScalarAsync(
            sql, new
            {
                Id = id,
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            });
    }

    public async Task StoreRefreshToken(string token, Guid userId)
    {
        const string sqlInsertString =
        @"
            INSERT INTO refresh_token
                (id, user_id, refresh_token)
            VALUES
                (@Id, @UserId, @Token)
        ";

        var id = Guid.NewGuid();

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteScalarAsync(
            sqlInsertString, new
            {
                Id = id,
                Token = token,
                UserId = userId
            });
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        const string sqlQueryString =
        @"
            SELECT *
            FROM user
            WHERE email = @Email
        ";

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(
            sqlQueryString, new
            {
                Email = email
            });
    }

    public async Task<User> GetUserByUserId(Guid userId)
    {
        const string sqlQueryString =
        @"
            SELECT *
            FROM user
            WHERE id = @UserId
        ";

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(
            sqlQueryString, new
            {
                UserId = userId
            });
    }

    public async Task LogoutUserCurrentDevice(Guid userId, string refreshToken)
    {
        const string sqlDeleteString =
        @"
            DELETE
            FROM refresh_token
            WHERE user_id = @UserId
            AND refresh_token = @RefreshToken
        ";

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteScalarAsync(
            sqlDeleteString, new
            {
                UserId = userId,
                RefreshToken = refreshToken
            });
    }

    public async Task LogoutUserAllDevices(Guid userId)
    {
        const string sqlDeleteString =
        @"
            DELETE
            FROM refresh_token
            WHERE user_id = @UserId
        ";

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteScalarAsync(
            sqlDeleteString, new
            {
                UserId = userId
            });
    }

    public async Task ChangeUserPassword(Guid userId, string hashedPassword)
    {
        const string sqlUpdateString =
        @"
            UPDATE user
            SET password = @HashedPassword
            WHERE id = @UserId
        ";

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteScalarAsync(
            sqlUpdateString, new
            {
                UserId = userId,
                HashedPassword = hashedPassword
            });
    }

    public async Task<IEnumerable<string>> GetRefreshTokensByUserId(Guid userId)
    {
        const string sqlQueryString =
        @"
            SELECT refresh_token
            FROM refresh_token
            WHERE user_id = @UserId
        ";

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        return await connection.QueryAsync<string>(
            sqlQueryString, new
            {
                UserId = userId,
            });
    }

    public async Task UpdateStoredRefreshToken(string oldToken, string newToken, Guid userId)
    {
        const string sqlUpdateString =
        @"
            UPDATE refresh_token rt
                SET rt.refresh_token = @NewToken
            WHERE rt.refresh_token = @OldToken
                AND rt.user_id = @UserId
        ";

        string connectionString = _config.GetConnectionString(ConnectionStringName);
        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteScalarAsync(
            sqlUpdateString, new
            {
                OldToken = oldToken,
                NewToken = newToken,
                UserId = userId
            });
    }
}