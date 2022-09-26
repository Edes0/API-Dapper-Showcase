namespace Boilerplate.Infrastructure.Identity;

using System;
using Boilerplate.Domain.Models;
using Domain.DomainModels;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Boilerplate> DbSetBoilerPlateModel { get; set; } = null!;
    public DbSet<User> DbSetUserModel { get; set; } = null!;
    public DbSet<RefreshToken> DbSetRefreshTokenModel { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding depending on environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Development")
        {
            Seed.SeedDataBase(modelBuilder);
        }

        Boilerplate.Configure(modelBuilder);
        User.Configure(modelBuilder);
        RefreshToken.Configure(modelBuilder);
    }
}