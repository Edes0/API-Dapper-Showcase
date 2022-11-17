namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Wash_Stats")]
public class WashStats
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Type", TypeName = "nvarchar(50)")]
    public string Type { get; set; }

    [Column("water_saved", TypeName = "float")]
    public float WaterSaved { get; set; }

    [Column("co2_saved", TypeName = "float")]
    public float Co2Saved { get; set; }

    [Column("plastic_saved", TypeName = "float")]
    public float PlasticSaved { get; set; }

    [Column("Economy_saved", TypeName = "float")]
    public float EconomySaved { get; set; }

    [Column("Started_At", TypeName = "datetime")]
    public DateTime StartedAt { get; set; }

    [Column("Ended_At", TypeName = "datetime")]
    public DateTime EndedAt { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }
}
