namespace Mimbly.Domain.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("Mimbly")]
public class Mimbly
{
    [Key]
    [Column("id", TypeName = "CHAR(36)", Order = 1)]
    public Guid Id { get; set; }

    [Column("first_name", TypeName = "Char(108)")]
    public string? FirstName { get; set; }

    [Column("last_name", TypeName = "Char(108)")]
    public string? LastName { get; set; }

    [Column("age", TypeName = "TINYINT")]
    public int Age { get; set; }

    /**
    * Model configurations.
    *
    * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
    */
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mimbly>()
            .HasIndex(x => x.Age);
    }
}