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

    /// <summary>
    /// Method <c>GetAllCompanyContacts</c> queries the database
    /// for all <c>CompanyContact</c>.
    /// </summary>
    /// <returns>A <c>IEnumerable</c> of <c>CompanyContact</c>.</returns>
    public async Task<IEnumerable<CompanyContact>> GetAllCompanyContacts()
    {
        var sql =
        @"
            SELECT *
            FROM Company_Contact
        ";

        return await _db.LoadEntities<CompanyContact, dynamic>(sql, new { });
    }

    /// <summary>
    /// Method <c>GetCompanyContactById</c> queries the database
    /// for a single <c>CompanyContact</c> using the provided <c>companyContactId</c>
    /// </summary>
    /// <param name="id">The <c>CompanyContact</c> to look for.</param>
    /// <returns>A single <c>CompanyContact</c>.</returns>
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

    public async Task<IEnumerable<CompanyContact>> GetCompanyContactsByCompanyId(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Company_Contact
            WHERE Company_Id = @id
        ";

        return await _db.LoadEntities<CompanyContact, dynamic>(sql, new { id });
    }

    /// <summary>
    /// Method <c>CreateCompanyContact</c> inserts a single <c>CompanyContact</c>
    /// into the database.
    /// </summary>
    /// <param name="companyContact">The <c>CompanyContact</c> to be inserted.</param>
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

    /// <summary>
    /// Method <c>DeleteCompanyContact</c> removes the provided
    /// <c>CompanyContact</c> from the database.
    /// </summary>
    /// <param name="companyContact">The <c>CompanyContact</c> to be removed.</param>
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

    /// <summary>
    /// Method <c>UpdateCompanyContact</c> updates the specified
    /// <c>CompanyContact</c> in the database with new values.
    /// </summary>
    /// <param name="companyContact">The <c>CompanyContact</c> to be updated.</param>
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
    /// <summary>
    /// Method <c>GetCompanyContactsByCompanyIds</c> queries the database
    /// for a list of <c>CompanyContact</c> that belongs to different companies
    /// by using the provided list of <c>companyId</c>.
    /// </summary>
    /// <param name="ids">The list of <c>companyId</c> to query for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>CompanyContact</c>.</returns>
    public async Task<IEnumerable<CompanyContact>> GetCompanyContactsByCompanyIds(IEnumerable<Guid> ids)
    {
        var sql =
        @"
            SELECT *
            FROM Company_Contact
            WHERE Company_Id IN @ids
        ";

        return await _db.LoadEntities<CompanyContact, dynamic>(sql, new { ids });
    }
}
