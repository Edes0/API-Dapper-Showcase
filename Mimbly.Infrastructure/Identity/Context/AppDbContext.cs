namespace Mimbly.Infrastructure.Identity.Context;

using System;
using Microsoft.EntityFrameworkCore;
using Mimbly.Domain.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Mimbox> Mimboxes { get; set; } = null!;
    public DbSet<MimboxLog> MimboxLogs { get; set; } = null!;
    public DbSet<MimboxModel> Models { get; set; } = null!;
    public DbSet<MimboxStatus> Status { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<CompanyContact> CompanyContacts { get; set; } = null!;
    public DbSet<MimboxLocation> Locations { get; set; } = null!;
    public DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
    public DbSet<EventLog> EventLogs { get; set; } = null!;
    public DbSet<WashStats> WashStats { get; set; } = null!;

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
        EventLog.Configure(modelBuilder);
        ErrorLog.Configure(modelBuilder);
    }
}