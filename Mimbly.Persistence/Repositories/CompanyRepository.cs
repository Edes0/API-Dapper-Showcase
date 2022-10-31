namespace Mimbly.Persistence.Repositories;

using System;
using System.Data.SqlClient;
using Application.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Mimbly.Domain.Entities;

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

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        var sql =
        @"
            SELECT *
            FROM Company
        ";

        return await _db.LoadEntities<Company, dynamic>(sql, new { });
    }

    public async Task<Company> GetCompanyById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Company
            WHERE Id = @id
        ";

        return await _db.LoadEntity<Company, dynamic>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Company>> GetParentWithChildrenById(Guid id)
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

    public async Task<IEnumerable<Company>> GetCompanyDataById(IEnumerable<Guid> ids)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT c.*,  cc.*, ccl.*
            FROM Company c
            LEFT JOIN Company cc ON c.Id = cc.Parent_Id
            LEFT JOIN Company_Contact ccl ON ccl.Company_Id = c.Id
            WHERE c.Id IN @ids
        ";

        var lookup = new Dictionary<Guid, Company>();

        await connection.QueryAsync<Company, Company, CompanyContact, Company>
           (sql, (company, childCompany, companyContact) =>
           {
               Company companyRef;

               if (!lookup.TryGetValue(company.Id, out companyRef))
                   lookup.Add(company.Id, companyRef = company);

               if (companyContact != null && !companyRef.ContactList.Select(x => x.Id).Contains(companyContact.Id))
                   companyRef.ContactList.Add(companyContact);

               return companyRef; //TODO: maybe null?
           },
           new
           {
               ids
           });

        return lookup.Values;
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

        await _db.SaveChanges(sql, company);
    }

    public async Task DeleteCompany(Company company)
    {
        var sql =
        @"
            DELETE
            FROM Company
            WHERE id = @Id
        ";

        await _db.SaveChanges(sql, company);
    }
}