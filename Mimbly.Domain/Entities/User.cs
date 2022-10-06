namespace Mimbly.Domain.Enitites;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("user")]
public class User
{
    [Key]
    [Column("id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Required]
    [EmailAddress]
    [Column("email", TypeName = "CHAR(128)")]
    public string Email { get; init; } = null!;

    [Required]
    [Column("password", TypeName = "CHAR(255)")]
    public string Password { get; init; } = null!;

    [Column("first_name", TypeName = "CHAR(128)")]
    public string? FirstName { get; init; }

    [Column("last_name", TypeName = "CHAR(128)")]
    public string? LastName { get; init; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }

    [Column("updated_at")] public DateTime UpdatedAt { get; set; }

    /**
     * Model configurations.
     *
     * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
     */
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>().Property(u => u.CreatedAt)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>().Property(u => u.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate();
    }
}