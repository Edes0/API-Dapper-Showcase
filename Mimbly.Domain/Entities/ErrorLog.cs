namespace Mimbly.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("Error_Log")]
public class ErrorLog
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Severity", TypeName = "nvarchar(50)")]
    public string Severity { get; set; }

    [Column("Message", TypeName = "nvarchar(max)")]
    public string Message { get; set; }

    [Column("Discarded", TypeName = "bit")]
    public  bool Discarded { get; set; }

    [Column("Created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLog>(entity => entity
        .Property(x => x.Created)
        .ValueGeneratedOnAdd());
    }
}
