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

    public async Task<IEnumerable<Company>> GetCompanyById(Guid Id)
    {
        var sql =
        @"
            SELECT *
            FROM Company
            WHERE id = @Id
        ";

        return await _db.LoadData<Company, dynamic>(sql, new { id = Id });
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