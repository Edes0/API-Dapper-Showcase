namespace Mimbly.Infrastructure.Identity.Context;

using System;
using Microsoft.EntityFrameworkCore;
using Mimbly.Domain.Enitites;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Mimbox> DbSetMimblyModel { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding depending on environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Development")
        {
            Seed.SeedDataBase(modelBuilder);
        }

        Mimbox.Configure(modelBuilder);
    }
}