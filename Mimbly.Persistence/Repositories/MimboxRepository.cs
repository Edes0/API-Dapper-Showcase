namespace Mimbly.Persistence.Repositories;

using System;
using System.Data.SqlClient;
using Application.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Mimbly.Domain.Entities;

public class MimboxRepository : IMimboxRepository
{
    private readonly ISqlDataAccess _db;
    private readonly IConfiguration _config;
    public string ConnectionStringName { get; set; } = "DbConnectionString";

    public MimboxRepository(
        ISqlDataAccess db,
        IConfiguration config)
    {
        _db = db;
        _config = config;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Mimbox>> GetAllMimboxes()
    {
        var sql =
        @"
            SELECT *
            FROM Mimbox
        ";

        return await _db.LoadEntities<Mimbox, dynamic>(sql, new { });
    }

    public async Task<Mimbox> GetMimboxById(Guid id)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT m.*, ml.*, ms.*, mm.*, mc.*, c.Id
            FROM Mimbox m
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Mimbox_Contact mc ON mc.Mimbox_Id = m.Id
            LEFT JOIN Company c ON c.Id = m.Company_Id
            WHERE m.Id = @id
        ";

        List<Mimbox> mimboxToReturn = new();

        await connection.QueryAsync<Mimbox, MimboxLocation, MimboxStatus, MimboxModel, MimboxContact, Company, Mimbox>
           (sql, (mimbox, mimboxLocation, mimboxStatus, mimboxModel, mimboxContact, company) =>
           {
               if (mimboxLocation != null)
               {
                   mimbox.Location = mimboxLocation;
                   mimbox.LocationId = mimboxLocation.Id;
               }

               if (company != null)
               {
                   mimbox.CompanyId = company.Id;
               }

               mimbox.Model = mimboxModel;
               mimbox.ModelId = mimboxModel.Id;
               mimbox.Status = mimboxStatus;
               mimbox.StatusId = mimboxStatus.Id;
               mimbox.ContactList.Add(mimboxContact);

               mimboxToReturn.Add(mimbox);

               return null;
           },
           new
           {
               id
           });

        return mimboxToReturn.FirstOrDefault();
    }

    public async Task CreateMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            INSERT INTO Mimbox
                (Id, Water_Saved, Co2_Saved, Plastic_Saved, Economy_Saved, Nickname, Mimbox_Status_Id, Mimbox_Model_Id, Mimbox_Location_Id, Company_Id)
            VALUES
                (@Id, @WaterSaved, @Co2Saved, @PlasticSaved, @EconomySaved, @Nickname, @StatusId, @ModelId, @LocationId, @CompanyId)
        ";

        await _db.SaveChanges(sql, mimbox);
    }

    public async Task DeleteMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            DELETE
            FROM Mimbox
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimbox);
    }

    public async Task UpdateMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            UPDATE Mimbox
            SET Water_Saved = @WaterSaved,
                Co2_Saved = @Co2Saved,
                Plastic_Saved = @PlasticSaved,
                Economy_Saved = @EconomySaved,
                Nickname = @Nickname,
                Mimbox_Status_Id = @StatusId,
                Mimbox_Model_Id = @ModelId,
                Mimbox_Location_Id = @LocationId,
                Company_Id = @CompanyId
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimbox);
    }

    public async Task<IEnumerable<Company>> GetMimboxDataByCompanyId(IEnumerable<Guid> ids)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT c.Id, m.*, ml.*, ms.*, mm.*, mc.*
            FROM Company c
            LEFT JOIN Mimbox m ON m.Company_Id = c.Id
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Mimbox_Contact mc ON mc.Mimbox_Id = m.Id
            WHERE c.Id IN @ids
        ";

        var lookup = new Dictionary<Guid, Company>();

        await connection.QueryAsync<Company, Mimbox, MimboxLocation, MimboxStatus, MimboxModel, MimboxContact, Company>
           (sql, (company, mimbox, mimboxLocation, mimboxStatus, mimboxModel, mimboxContact) =>
           {
               Company companyRef;

               if (!lookup.TryGetValue(company.Id, out companyRef))
                   lookup.Add(company.Id, companyRef = company);

               if (mimbox != null && !companyRef.MimboxList.Select(x => x.Id).Contains(mimbox.Id))
               {
                   if (mimboxLocation != null)
                   {
                       mimbox.Location = mimboxLocation;
                       mimbox.LocationId = mimboxLocation.Id;
                   }

                   mimbox.Model = mimboxModel;
                   mimbox.ModelId = mimboxModel.Id;
                   mimbox.Status = mimboxStatus;
                   mimbox.StatusId = mimboxStatus.Id;
                   mimbox.ContactList.Add(mimboxContact);

                   companyRef.MimboxList.Add(mimbox);
               }
               return null;
           },
           new
           {
               ids
           });

        return lookup.Values;
    }
}