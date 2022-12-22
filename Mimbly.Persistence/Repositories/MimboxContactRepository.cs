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

    public async Task<IEnumerable<MimboxContact>> GetAllMimboxContacts()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox_Contact
        ";

        return await _db.LoadEntities<MimboxContact, dynamic>(sql, new { });
    }

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
}