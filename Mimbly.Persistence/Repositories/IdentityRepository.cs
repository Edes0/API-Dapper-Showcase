namespace Mimbly.Persistence.Repositories;

using System.Security.Claims;
using Application.Common.Interfaces;
using Application.Common.ServiceOptions;
using Dapper;
using Domain.DomainModels;
using MySql.Data.MySqlClient;

public class IdentityRepository : IIdentityRepository
{
    private readonly ConnectionStrings _connectionOptions;

    public IdentityRepository(ConnectionStrings connectionOptions)
    {
        _connectionOptions = connectionOptions;
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
        const string insertUserSql =
        @"
            INSERT INTO user
                (id, email, password, first_name, last_name)
            VALUES
                (@Id, @Email, @Password, @FirstName, @LastName)
        ";

        var id = Guid.NewGuid();

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        await dbConnection.ExecuteScalarAsync(
            insertUserSql, new
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

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        await dbConnection.ExecuteScalarAsync(
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

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        return await dbConnection.QueryFirstOrDefaultAsync<User>(
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

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        return await dbConnection.QueryFirstOrDefaultAsync<User>(
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

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        await dbConnection.ExecuteScalarAsync(
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

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        await dbConnection.ExecuteScalarAsync(
            sqlDeleteString, new
            {
                UserId = userId
            });
    }

    public async Task CreateInvitedUsers(IEnumerable<User> newUsers)
    {
        const string insertUserSql =
        @"
            INSERT INTO user
                (id, email, password, first_name, last_name)
            VALUES
                (@Id, @Email, @Password, @FirstName, @LastName)
        ";

        await using var dbCon = new MySqlConnection(_connectionOptions.DbConnectionString);
        await dbCon.OpenAsync();
        await using var transaction = await dbCon.BeginTransactionAsync();

        foreach (var user in newUsers)
        {
            await dbCon.ExecuteScalarAsync(insertUserSql, new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Password,
            }, transaction);
        }

        await transaction.CommitAsync();
    }

    public async Task ChangeUserPassword(Guid userId, string hashedPassword)
    {
        const string sqlUpdateString =
        @"
            UPDATE user
            SET password = @HashedPassword
            WHERE id = @UserId
        ";

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        await dbConnection.ExecuteScalarAsync(
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

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        return await dbConnection.QueryAsync<string>(
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

        await using var dbConnection = new MySqlConnection(_connectionOptions.DbConnectionString);
        await dbConnection.ExecuteScalarAsync(
            sqlUpdateString, new
            {
                OldToken = oldToken,
                NewToken = newToken,
                UserId = userId
            });
    }
}