namespace Mimbly.Persistence.Repositories;

using System;
using Application.Common.Interfaces;
using Dapper;
using Mimbly.Domain.Entities;

public class CompanyRepository : ICompanyRepository
{
    private readonly ISqlDataAccess _db;
    public string ConnectionStringName { get; set; } = "DbConnectionString";


    public CompanyRepository(ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        var sql =
        @"
            SELECT *
            FROM Company
        ";

        return await _db.LoadData<Company, dynamic>(sql, new { });
    }

    public async Task<IEnumerable<Company>> GetCompanyById(Guid id)
    {
        var sql =
        @"

            WHERE Id = @id
        ";

        return await _db.LoadData<Company, dynamic>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Company>> GetCompanyWithChildrenById(Guid id)
    {
        var sql =
        @"
   WITH Children AS
(
    SELECT *
        FROM Company WHERE Parent_Id = @id
    UNION ALL
    SELECT Company.* FROM Company  JOIN Children  ON Company.Parent_Id = Children.Id
)
SELECT*
    FROM Children

OPTION(MAXRECURSION 32767)
        ";

        return await _db.LoadData<Company, dynamic>(sql, new { });
    }

    public async Task CreateCompany(Company company)
    {
        var sql =
        @"
            INSERT INTO Company
                (id, first_name, last_name, age)
            VALUES
                (@Id, @FirstName, @LastName, @Age)
        ";

        await _db.SaveData(sql, company);
    }

    public async Task DeleteCompany(Company company)
    {
        var sql =
        @"
            DELETE
            FROM Company
            WHERE id = @Id
        ";

        await _db.SaveData(sql, company);
    }
}