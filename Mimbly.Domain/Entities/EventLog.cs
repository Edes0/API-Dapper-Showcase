namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Event_Log")]
public class EventLog
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Type", TypeName = "nvarchar(50)")]
    public string Type { get; set; }

    [Column("Log", TypeName = "nvarchar(max)")]
    public string Log { get; set; }

    [Column("Created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventLog>(entity => entity
        .Property(x => x.Created)
        .ValueGeneratedOnAdd());
    }
}



