namespace Mimbly.Persistence.Repositories;

using System;
using System.Data.SqlClient;
using System.Linq;
using Application.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Mimbly.Domain.Entities;
using Mimbly.Domain.Entities.AzureEvents;

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

    public async Task CreateMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            INSERT INTO Mimbox
                (Id, Water_Saved, Co2_Saved, Plastic_Saved, Economy_Saved, Nickname, Mimbox_Status_Id, Mimbox_Model_Id, Mimbox_Location_Id, Company_Id, Stats_Updated_At)
            VALUES
                (@Id, @WaterSaved, @Co2Saved, @PlasticSaved, @EconomySaved, @Nickname, @StatusId, @ModelId, @LocationId, @CompanyId, @StatsUpdatedAt)
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

    public async Task<IEnumerable<Mimbox>> GetAllMimboxes()
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT m.*, ml.*,ms.*, mm.*, mc.*, mel.*, c.Id
            FROM Mimbox m
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Contact mc ON mc.Mimbox_Id = m.Id
            LEFT JOIN Mimbox_Error_Log mel ON mel.Mimbox_Id = m.Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Company c ON c.Id = m.Company_Id
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Mimbox, MimboxLocation, MimboxStatus, MimboxModel, MimboxContact, MimboxErrorLog, Company, Mimbox>
           (sql, (mimbox, mimboxLocation, mimboxStatus, mimboxModel, mimboxContact, mimboxErrorLog, company) =>
           {
               if (!lookup.TryGetValue(mimbox.Id, out var mimboxRef))
                   lookup.Add(mimbox.Id, mimboxRef = mimbox);

               if (company != null)
               {
                   mimboxRef.Company = company;
               }

               if (mimboxLocation != null)
               {
                   mimboxRef.Location = mimboxLocation;
                   mimboxRef.LocationId = mimboxLocation.Id;
               }

               if (mimboxContact != null)
               {
                   mimboxRef.ContactList.Add(mimboxContact);
               }

               if (mimboxErrorLog != null)
               {
                   mimboxRef.ErrorLogList.Add(mimboxErrorLog);
               }

               mimboxRef.Status = mimboxStatus;
               mimboxRef.StatusId = mimboxStatus.Id;
               mimboxRef.Model = mimboxModel;
               mimboxRef.ModelId = mimboxModel.Id;

               return null;
           });

        return lookup.Values;
    }

    public async Task<Mimbox> GetMimboxById(Guid id)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT m.*, ml.*, mc.*, mel.*, ms.*, mm.*, mlo.*, c.Id
            FROM Mimbox m
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Contact mc ON mc.Mimbox_Id = m.Id
            LEFT JOIN Mimbox_Error_Log mel ON mel.Mimbox_Id = m.Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Mimbox_Log mlo ON mlo.Mimbox_Id = m.Id
            LEFT JOIN Company c ON c.Id = m.Company_Id
            WHERE m.Id = @id
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Mimbox>
           (sql, new[]
             {
               typeof(Mimbox),
               typeof(MimboxLocation),
               typeof(MimboxContact),
               typeof(MimboxErrorLog),
               typeof(MimboxStatus),
               typeof(MimboxModel),
               typeof(MimboxLog),
               typeof(Company)
               }
           , obj =>
           {
               Mimbox mimbox = obj[0] as Mimbox;
               MimboxLocation? mimboxLocation = obj[1] as MimboxLocation;
               MimboxContact? mimboxContact = obj[2] as MimboxContact;
               MimboxErrorLog? mimboxErrorLog = obj[3] as MimboxErrorLog;
               MimboxStatus mimboxStatus = obj[4] as MimboxStatus;
               MimboxModel mimboxModel = obj[5] as MimboxModel;
               MimboxLog mimboxLog = obj[6] as MimboxLog;
               Company company = obj[7] as Company;


               if (!lookup.TryGetValue(mimbox.Id, out var mimboxRef))
                   lookup.Add(mimbox.Id, mimboxRef = mimbox);

               if (mimboxLocation != null)
               {
                   mimboxRef.Location = mimboxLocation;
                   mimboxRef.LocationId = mimboxLocation.Id;
               }

               if (mimboxContact != null)
               {
                   mimboxRef.ContactList.Add(mimboxContact);
               }

               if (mimboxErrorLog != null)
               {
                   mimboxRef.ErrorLogList.Add(mimboxErrorLog);
               }

               if (mimboxLog != null)
               {
                   mimboxRef.LogList.Add(mimboxLog);
               }

               if (company != null)
               {
                   mimboxRef.Company = company;
               }

               mimboxRef.Model = mimboxModel;
               mimboxRef.ModelId = mimboxModel.Id;
               mimboxRef.Status = mimboxStatus;
               mimboxRef.StatusId = mimboxStatus.Id;

               return null;
           },
           new { id });

        return lookup.Values.FirstOrDefault();
    }

    public async Task<IEnumerable<Mimbox>> GetMimboxByCompanyIds(IEnumerable<Guid> ids)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT c.Id, m.*, ml.*, ms.*, mm.*, mc.*, mel.*
            FROM Company c
            LEFT JOIN Mimbox m ON m.Company_Id = c.Id
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Mimbox_Contact mc ON mc.Mimbox_Id = m.Id
            LEFT JOIN Mimbox_Error_Log mel ON mel.Mimbox_Id = m.Id
            WHERE c.Id IN @ids
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Company, Mimbox, MimboxLocation, MimboxStatus, MimboxModel, MimboxContact, MimboxErrorLog, Company>
           (sql, (company, mimbox, mimboxLocation, mimboxStatus, mimboxModel, mimboxContact, mimboxErrorLog) =>
           {
               if (mimbox != null)
               {
                   if (!lookup.TryGetValue(mimbox.Id, out var mimboxRef))
                       lookup.Add(mimbox.Id, mimboxRef = mimbox);

                   if (mimboxLocation != null)
                   {
                       mimboxRef.Location = mimboxLocation;
                       mimboxRef.LocationId = mimboxLocation.Id;
                   }

                   if (mimboxErrorLog != null)
                   {
                       mimboxRef.ErrorLogList.Add(mimboxErrorLog);
                   }

                   mimboxRef.Model = mimboxModel;
                   mimboxRef.ModelId = mimboxModel.Id;
                   mimboxRef.Status = mimboxStatus;
                   mimboxRef.StatusId = mimboxStatus.Id;
                   mimboxRef.CompanyId = company.Id;
               }
               return null;
           },
           new { ids });

        return lookup.Values;
    }

    public async Task<IEnumerable<Mimbox>> GetMimboxByCompanyId(Guid id)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT c.Id, m.*, ml.*, ms.*, mm.*, mc.*, mel.*
            FROM Company c
            LEFT JOIN Mimbox m ON m.Company_Id = c.Id
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Mimbox_Contact mc ON mc.Mimbox_Id = m.Id
            LEFT JOIN Mimbox_Error_Log mel ON mel.Mimbox_Id = m.Id
            WHERE c.Id = @id
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Company, Mimbox, MimboxLocation, MimboxStatus, MimboxModel, MimboxContact, MimboxErrorLog, Company>
           (sql, (company, mimbox, mimboxLocation, mimboxStatus, mimboxModel, mimboxContact, mimboxErrorLog) =>
           {
               if (mimbox != null)
               {
                   if (!lookup.TryGetValue(mimbox.Id, out var mimboxRef))
                       lookup.Add(mimbox.Id, mimboxRef = mimbox);

                   if (mimboxLocation != null)
                   {
                       mimboxRef.Location = mimboxLocation;
                       mimboxRef.LocationId = mimboxLocation.Id;
                   }

                   if (mimboxContact != null && !mimboxRef.ContactList.Any(x => x.Id == mimboxContact.Id))
                       mimboxRef.ContactList.Add(mimboxContact);

                   if (mimboxErrorLog != null && !mimboxRef.ErrorLogList.Any(x => x.Id == mimboxErrorLog.Id))
                       mimboxRef.ErrorLogList.Add(mimboxErrorLog);

                   mimboxRef.Model = mimboxModel;
                   mimboxRef.ModelId = mimboxModel.Id;
                   mimboxRef.Status = mimboxStatus;
                   mimboxRef.StatusId = mimboxStatus.Id;
                   mimboxRef.CompanyId = company.Id;
               }
               return null;
           },
           new { id });

        return lookup.Values;
    }
}
