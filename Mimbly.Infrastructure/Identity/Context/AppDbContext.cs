namespace Mimbly.Infrastructure.Identity.Context;

using System;
using Microsoft.EntityFrameworkCore;
using Mimbly.Domain.Entities;
using NLog.Filters;

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
    public DbSet<Location> Locations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding depending on environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Development")
        {
            Seed.SeedDataBase(modelBuilder);
        }

        // Building Company enitity relationships
        modelBuilder.Entity<Company>(company => company.HasMany(x => x.ContactList).WithOne(c => c.Company));

        // Building Mimbox enitity relationships
        modelBuilder.Entity<Mimbox>(mimbox => mimbox.HasOne(x => x.Model).WithMany(c => c.Mimboxes));
        modelBuilder.Entity<Mimbox>(mimbox => mimbox.HasOne(x => x.Status).WithMany(c => c.Mimboxes));
        modelBuilder.Entity<Mimbox>(mimbox => mimbox.HasOne(x => x.Location).WithMany(c => c.Mimboxes));
        modelBuilder.Entity<Mimbox>(mimbox => mimbox.HasOne(x => x.Company).WithMany(c => c.MimboxList));
        //modelBuilder.Entity<Mimbox>(mimbox => mimbox.HasMany(x => x.MimboxLogList).WithOne(c => c.Mimbox));

        // Building MimboxLog property settings
        modelBuilder.Entity<MimboxLog>(entity => entity.Property(x => x.Created)
            .ValueGeneratedOnAdd());

        // Building MimboxStatus property settings
        modelBuilder.Entity<MimboxStatus>(entity => entity.Property(x => x.Updated)
            .ValueGeneratedOnUpdate());
    }
}