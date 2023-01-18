namespace Mimbly.Persistence.Repositories;

using System;
using Application.Common.Interfaces;
using Dapper;
using Mimbly.Domain.Entities;

public class MimboxContactRepository : IMimboxContactRepository
{
    private readonly ISqlDataAccess _db;

    public MimboxContactRepository(
        ISqlDataAccess db)
    {
        _db = db;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <summary>
    /// Method <c>GetAllMimboxContacts</c> queries the database
    /// for all <c>MimboxContact</c>.
    /// </summary>
    /// <returns>A <c>IEnumerable</c> of <c>MimboxContact</c>.</returns>
    public async Task<IEnumerable<MimboxContact>> GetAllMimboxContacts()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Contact
        ";

        return await _db.LoadEntities<MimboxContact, dynamic>(sql, new { });
    }

    /// <summary>
    /// Method <c>GetMimboxContactById</c> queries the database
    /// for a single <c>MimboxContact</c> using the provided <c>mimboxContactId</c>
    /// </summary>
    /// <param name="id">The <c>MimboxContact</c> to look for.</param>
    /// <returns>A single <c>MimboxContact</c>.</returns>
    public async Task<MimboxContact> GetMimboxContactById(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Contact
            WHERE Id = @id
        ";

        return await _db.LoadEntity<MimboxContact, dynamic>(sql, new { Id = id });
    }

    /// <summary>
    /// Method <c>CreateMimboxContact</c> inserts a single <c>MimboxContact</c>
    /// into the database.
    /// </summary>
    /// <param name="mimboxContact">The <c>MimboxContact</c> to be inserted.</param>
    public async Task CreateMimboxContact(MimboxContact mimboxContact)
    {
        var sql =
        @"
            INSERT INTO Mimbox_Contact
                (Id, Title, First_name, Last_name, Email, Phone_number, Mimbox_Id)
            VALUES
                (@Id, @Title, @FirstName, @LastName, @Email, @PhoneNumber, @MimboxId)
        ";

        await _db.SaveChanges(sql, mimboxContact);
    }

    /// <summary>
    /// Method <c>DeleteMimboxContact</c> removes the provided
    /// <c>MimboxContact</c> from the database.
    /// </summary>
    /// <param name="mimboxContact">The <c>MimboxContact</c> to be removed.</param>
    public async Task DeleteMimboxContact(MimboxContact mimboxContact)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox_Contact
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxContact);
    }

    /// <summary>
    /// Method <c>UpdateMimboxContact</c> updates the specified
    /// <c>MimboxContact</c> in the database with new values.
    /// </summary>
    /// <param name="mimboxContact">The <c>MimboxContact</c> to be updated.</param>
    public async Task UpdateMimboxContact(MimboxContact mimboxContact)
    {
        var sql =
        @"
            UPDATE Mimbox_Contact
            SET Title = @Title,
                First_name = @FirstName,
                Last_name = @LastName,
                Email = @Email,
                Phone_number = @PhoneNumber,
                Mimbox_Id = @MimboxId
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimboxContact);
    }

    /// <summary>
    /// Method <c>GetMimboxContactsByMimboxId</c> queries the database
    /// for a list of <c>MimboxContact</c> using the provided <c>mimboxId</c>
    /// </summary>
    /// <param name="id">The <c>mimboxId</c> to look for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>MimboxContact</c>.</returns>
    public async Task<IEnumerable<MimboxContact>> GetMimboxContactsByMimboxId(Guid id)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Contact
            WHERE Mimbox_Id = @id
        ";

        return await _db.LoadEntities<MimboxContact, dynamic>(sql, new { id });
    }

    /// <summary>
    /// Method <c>GetMimboxContactsByMimboxIds</c> queries the database
    /// for a list of <c>MimboxContact</c> that belongs to different mimboxes
    /// by using the provided list of <c>mimboxId</c>.
    /// </summary>
    /// <param name="ids">The list of <c>mimboxId</c> to query for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>MimboxContact</c>.</returns>
    public async Task<IEnumerable<MimboxContact>> GetMimboxContactsByMimboxIds(IEnumerable<Guid> ids)
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Contact
            WHERE Mimbox_Id IN @ids
        ";

        return await _db.LoadEntities<MimboxContact, dynamic>(sql, new { ids });
    }
}