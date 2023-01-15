namespace Mimbly.Persistence.Repositories;

using System;
using System.Data.SqlClient;
using System.Linq;
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
            SELECT m.*, ml.*,ms.*, mm.*, c.Id
            FROM Mimbox m
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Company c ON c.Id = m.Company_Id
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Mimbox, MimboxLocation, MimboxStatus, MimboxModel, Company, Mimbox>
           (sql, (mimbox, mimboxLocation, mimboxStatus, mimboxModel, company) =>
           {
               if (!lookup.TryGetValue(mimbox.Id, out var mimboxRef))
                   lookup.Add(mimbox.Id, mimboxRef = mimbox);

               if (mimboxLocation != null)
               {
                   mimboxRef.Location = mimboxLocation;
                   mimboxRef.LocationId = mimboxLocation.Id;
               }

               if (company != null)
                   mimboxRef.Company = company;

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
            SELECT m.*, ml.*, ms.*, mm.*, c.Id
            FROM Mimbox m
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            LEFT JOIN Company c ON c.Id = m.Company_Id
            WHERE m.Id = @id
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Mimbox, MimboxLocation, MimboxStatus, MimboxModel, Company, Mimbox>
           (sql, (mimbox, mimboxLocation, mimboxStatus, mimboxModel, company) =>
           {
               if (!lookup.TryGetValue(mimbox.Id, out var mimboxRef))
                   lookup.Add(mimbox.Id, mimboxRef = mimbox);

               if (mimboxLocation != null)
               {
                   mimboxRef.Location = mimboxLocation;
                   mimboxRef.LocationId = mimboxLocation.Id;
               }

               if (company != null)
                   mimboxRef.Company = company;

               mimboxRef.Model = mimboxModel;
               mimboxRef.ModelId = mimboxModel.Id;
               mimboxRef.Status = mimboxStatus;
               mimboxRef.StatusId = mimboxStatus.Id;

               return null;
           },
           new { id });

        return lookup.Values.FirstOrDefault();
    }

    public async Task<IEnumerable<Mimbox>> GetMimboxesByCompanyIds(IEnumerable<Guid> ids)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT c.Id, m.*, ml.*, ms.*, mm.*
            FROM Company c
            LEFT JOIN Mimbox m ON m.Company_Id = c.Id
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            WHERE c.Id IN @ids
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Company, Mimbox, MimboxLocation, MimboxStatus, MimboxModel, Company>
           (sql, (company, mimbox, mimboxLocation, mimboxStatus, mimboxModel) =>
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

    public async Task<IEnumerable<Mimbox>> GetMimboxesByCompanyId(Guid id)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT c.Id, m.*, ml.*, ms.*, mm.*
            FROM Company c
            LEFT JOIN Mimbox m ON m.Company_Id = c.Id
            LEFT JOIN Mimbox_Location ml ON ml.Id = m.Mimbox_Location_Id
            LEFT JOIN Mimbox_Status ms ON ms.Id = m.Mimbox_Status_Id
            LEFT JOIN Mimbox_Model mm ON mm.Id = m.Mimbox_Model_Id
            WHERE c.Id = @id
        ";

        var lookup = new Dictionary<Guid, Mimbox>();

        await connection.QueryAsync<Company, Mimbox, MimboxLocation, MimboxStatus, MimboxModel, Company>
           (sql, (company, mimbox, mimboxLocation, mimboxStatus, mimboxModel) =>
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
