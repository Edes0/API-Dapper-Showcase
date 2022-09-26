namespace Boilerplate.Domain.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainModels;
using Microsoft.EntityFrameworkCore;

[Table("refresh_token")]
public class RefreshToken
{
    [Key]
    [Column("id", TypeName = "CHAR(36)", Order = 1)]
    public Guid Id { get; set; }

    [Column("user_id", TypeName = "Char(36)")]
    [Required]
    public Guid UserId { get; private set; }

    [Column("refresh_token", TypeName = "Char(255)")]
    [Required]
    public string Token { get; set; } = null!;

    [Column("token_set_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime TokenSetAt { get; set; }

    /**
     * Model configurations.
     *
     * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
     */
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>()
            .HasIndex(rf => rf.UserId);

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(rf => new { rf.Token, rf.UserId });

        modelBuilder.Entity<RefreshToken>()
            .Property(u => u.TokenSetAt)
            .ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<User>()
            .HasMany<RefreshToken>();
    }
}