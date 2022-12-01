namespace Mimbly.Infrastructure.Identity.Context;

using System;
using Microsoft.EntityFrameworkCore;
using Mimbly.Domain.Entities;
using Mimbly.Domain.Entities.AzureEvents;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Mimbox> Mimboxes { get; set; } = null!;
    public DbSet<MimboxLog> MimboxLogs { get; set; } = null!;
    public DbSet<MimboxModel> MimboxModels { get; set; } = null!;
    public DbSet<MimboxStatus> MimboxStatus { get; set; } = null!;
    public DbSet<MimboxErrorLog> MimboxErrorLogs { get; set; } = null!;
    public DbSet<MimboxLocation> MimboxLocations { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<CompanyContact> CompanyContacts { get; set; } = null!;
    public DbSet<EventLog> EventLogs { get; set; } = null!;
    public DbSet<WashStats> WaterToWashingMachineEvents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding depending on environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Development")
        {
            Seed.SeedDataBase(modelBuilder);
        }

        // Configure entities
        MimboxLog.Configure(modelBuilder);
    }
}