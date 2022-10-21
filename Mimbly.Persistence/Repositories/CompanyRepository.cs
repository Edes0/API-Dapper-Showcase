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


    public CompanyRepository(ISqlDataAccess db, IConfiguration config)
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

    public async Task<IEnumerable<Company>> GetParentWithChildrenById(Guid id)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
          @"
              WITH Children AS
                (
                SELECT *
                FROM Company WHERE Parent_Id = 'BB2A7361-75F4-4773-ADD0-F1A2E6A11C79' OR Id = 'BB2A7361-75F4-4773-ADD0-F1A2E6A11C79'
                UNION ALL
                SELECT Company.* FROM Company  JOIN Children  ON Company.Parent_Id = Children.Id
                )
                    SELECT DISTINCT *
                    FROM Children

                    OPTION(MAXRECURSION 32767)
          ";

        return await _db.LoadData<Company, dynamic>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Company>> GetCompanyDataById(IEnumerable<Guid> ids)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
             SELECT c.*,  cc.*, ccl.*, m.*
            FROM Company c
            LEFT JOIN Company cc ON c.Id = cc.Parent_Id
            LEFT JOIN Company_Contact ccl ON ccl.Company_Id = c.Id
            LEFT JOIN Mimbox m ON m.Company_Id = c.Id
            WHERE c.Id IN @ids
        ";

        var lookup = new Dictionary<Guid, Company>();

        await connection.QueryAsync<Company, Company, CompanyContact, Mimbox, Company>
           (sql, (company, childCompany, companyContact, mimbox) =>
           {
               Company companyRef;

               if (!lookup.TryGetValue(company.Id, out companyRef))
               {
                   lookup.Add(company.Id, companyRef = company);
               }

               //TODO: FIX METHOD
               if (mimbox != null && !companyRef.MimboxList.Select(x => x.Id).Contains(mimbox.Id))
                   companyRef.MimboxList.Add(mimbox);

               if (companyContact != null && !companyRef.ContactList.Select(x => x.Id).Contains(companyContact.Id))
                   companyRef.ContactList.Add(companyContact);

               return companyRef;
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