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

    /// <summary>
    /// Method <c>CreateMimbox</c> inserts a single <c>mimbox</c>
    /// into the database.
    /// </summary>
    /// <param name="mimbox">The <c>mimbox</c> to be inserted.</param>
    public async Task CreateMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            INSERT INTO Mimbox
                (Id, Water_Saved, Co2_Saved, Plastic_Saved, Economy_Saved, Total_Tap, Total_Washes, Nickname, Mimbox_Status_Id, Mimbox_Model_Id, Mimbox_Location_Id, Company_Id, Stats_Updated_At)
            VALUES
                (@Id, @WaterSaved, @Co2Saved, @PlasticSaved, @EconomySaved, @TotalTap, @TotalWashes, @Nickname, @StatusId, @ModelId, @LocationId, @CompanyId, @StatsUpdatedAt)
        ";

        await _db.SaveChanges(sql, mimbox);
    }

    /// <summary>
    /// Method <c>DeleteMimbox</c> removes the provided
    /// <c>mimbox</c> from the database.
    /// </summary>
    /// <param name="mimbox">The <c>mimbox</c> to be deleted.</param>
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

    /// <summary>
    /// Method <c>UpdateMimbox</c> updates the <c>mimbox</c> in
    /// the database with new values.
    /// </summary>
    /// <param name="mimbox">The <c>mimbox</c> to be updated.</param>
    public async Task UpdateMimbox(Mimbox mimbox)
    {
        var sql =
        @"
            UPDATE Mimbox
            SET Nickname = @Nickname,
                Mimbox_Status_Id = @StatusId,
                Mimbox_Model_Id = @ModelId,
                Mimbox_Location_Id = @LocationId,
                Company_Id = @CompanyId
            WHERE Id = @Id
        ";

        await _db.SaveChanges(sql, mimbox);
    }

    /// <summary>
    /// Method <c>GetAllMimboxes</c> queries the database
    /// for every <c>mimbox</c>.
    /// </summary>
    /// <returns>A <c>IEnumerable</c> of <c>mimbox</c>. The object <c>mimbox</c> includes values for
    /// <c>mimboxLocation</c>, <c>mimboxStatus</c>, <c>mimboxModel</c> and <c>company</c>.
    /// </returns>
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

    /// <summary>
    /// Method <c>GetMimboxById</c> queries the database
    /// for a single <c>mimbox</c>.
    /// </summary>
    /// <param name="id">The <c>mimboxId</c> to look for.</param>
    /// <returns>A single <c>mimbox</c>.</returns>
    public async Task<Mimbox> GetMimboxById(Guid id)
    {
        var connectionString = _config.GetConnectionString(ConnectionStringName);
        await using var connection = new SqlConnection(connectionString);

        var sql =
        @"
            SELECT m.*, ml.*, ms.*, mm.*, c.*
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

    /// <summary>
    /// Method <c>GetMimboxesByCompanyIds</c> queries the database
    /// for a list of <c>mimbox</c> that belongs to different companies
    /// by using the provided list of <c>companyId</c>.
    /// </summary>
    /// <param name="ids">The list of <c>companyId</c> to query for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>mimbox</c>. The object <c>mimbox</c> includes values for
    /// <c>mimboxLocation</c>, <c>mimboxStatus</c>, <c>mimboxModel</c> and <c>company</c>.
    /// </returns>
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

    /// <summary>
    /// Method <c>GetMimboxesByCompanyId</c> queries the database
    /// for a list of <c>mimbox</c> that belongs to a single company
    /// by using the provided <c>companyId</c>.
    /// </summary>
    /// <param name="id">The <c>companyId</c> to query for.</param>
    /// <returns>A <c>IEnumerable</c> of <c>mimbox</c>. The object <c>mimbox</c> includes values for
    /// <c>mimboxLocation</c>, <c>mimboxStatus</c>, <c>mimboxModel</c> and <c>company</c>.
    /// </returns>
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
