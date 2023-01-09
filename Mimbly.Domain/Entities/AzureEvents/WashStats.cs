namespace Mimbly.Domain.Entities.AzureEvents;

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

    [Column("Water_saved", TypeName = "float")]
    public float WaterSaved { get; set; }

    [Column("Co2_saved", TypeName = "float")]
    public float Co2Saved { get; set; }

    [Column("Plastic_saved", TypeName = "float")]
    public float PlasticSaved { get; set; }

    [Column("Economy_saved", TypeName = "float")]
    public float EconomySaved { get; set; }

    [Column("Started_At", TypeName = "datetime")]
    public DateTime StartedAt { get; set; } // Created at this time

    [Column("Ended_At", TypeName = "datetime")]
    public DateTime? EndedAt { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }

    [Column("Washing_Machine_Id", TypeName = "tinyint")]
    public int WashingMachineId { get; set; }
}
