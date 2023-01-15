namespace Mimbly.Persistence.Repositories;

using System;
using System.Data.SqlClient;
using Application.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Mimbly.Domain.Entities;
using Mimbly.Domain.Entities.AzureEvents;

public class CompanyRepository : ICompanyRepository
{
    private readonly ISqlDataAccess _db;
    private readonly IConfiguration _config;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public CompanyRepository(
        ISqlDataAccess db,
        IConfiguration config)
    {
        _db = db;
        _config = config;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }


    public async Task CreateCompany(Company company)
    {
        var sql =
        @"
            INSERT INTO Company
                (Id, Name, Parent_Id)
            VALUES
                (@Id, @Name, @ParentId)
        ";

        await _db.SaveChanges(sql, company);
    }

    public async Task DeleteCompany(Company company)
    {
        var sql =
        @"
            DELETE
            FROM Company
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, company);
    }

    public async Task UpdateCompany(Company company)
    {
        var sql =
        @"
            UPDATE Company
            SET Name = @Name,
                Parent_Id = @ParentId
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, company);
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        var sql =
        @"
            SELECT c.*
            FROM Company c
        ";

        return await _db.LoadEntities<Company, dynamic>(sql, new { });
    }

    public async Task<Company> GetCompanyById(Guid id)
    {
        var sql =
        @"
            SELECT c.*
            FROM Company c
            WHERE c.Id = @id
        ";

        return await _db.LoadEntity<Company, dynamic>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Company>> GetParentAndChildrenIdsById(Guid id)
    {
        var sql =
          @"
              WITH Children AS
                (
                SELECT *
                FROM Company WHERE Parent_Id = @id OR Id = @id
                UNION ALL
                SELECT Company.* FROM Company  JOIN Children  ON Company.Parent_Id = Children.Id
                )
                    SELECT DISTINCT *
                    FROM Children

                    OPTION(MAXRECURSION 32767)
          ";

        return await _db.LoadEntities<Company, dynamic>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Company>> GetCompanyByIds(IEnumerable<Guid> ids)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT c.*
            FROM Company c
            WHERE c.Id IN @ids
        ";

        return await _db.LoadEntities<Company, dynamic>(sql, new { ids });
    }
}
