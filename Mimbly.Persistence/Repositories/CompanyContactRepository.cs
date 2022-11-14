namespace Mimbly.Persistence.Repositories;

using System;
using Application.Common.Interfaces;
using Dapper;
using Mimbly.Domain.Entities;

public class CompanyContactRepository : ICompanyContactRepository
{
    private readonly ISqlDataAccess _db;

    public CompanyContactRepository(
        ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<CompanyContact>> GetAllCompanyContacts()
    {
        var sql =
        @"
            SELECT *
            FROM Company_Contact
        ";

        return await _db.LoadEntities<CompanyContact, dynamic>(sql, new { });
    }

    public async Task<CompanyContact> GetCompanyContactById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Company_Contact
            WHERE Id = @id
        ";

        return await _db.LoadEntity<CompanyContact, dynamic>(sql, new { Id = id });
    }
    public async Task CreateCompanyContact(CompanyContact companyContact)
    {
        var sql =
        @"
            INSERT INTO Company_Contact
                (Id, Title, First_name, Last_name, Email, Phone_number, Company_Id)
            VALUES
                (@Id, @Title, @FirstName, @LastName, @Email, @PhoneNumber, @CompanyId)
        ";

        await _db.SaveChanges(sql, companyContact);
    }

    public async Task DeleteCompanyContact(CompanyContact companyContact)
    {
        var sql =
        @"
            DELETE
            FROM Company_Contact
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, companyContact);
    }

    public async Task UpdateCompanyContact(CompanyContact companyContact)
    {
        var sql =
        @"
            UPDATE Company_Contact
            SET Title = @Title,
                First_name = @FirstName,
                Last_name = @LastName,
                Email = @Email,
                Phone_number = @PhoneNumber,
                Company_Id = @CompanyId
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, companyContact);
    }
}
