namespace Mimbly.Domain.Entities.AzureEvents;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Water_To_Mimbox_Event")]
public class WaterToMimboxEvent
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Started_At", TypeName = "datetime")]
    public DateTime StartedAt { get; set; }

    [Column("Ended_At", TypeName = "datetime")]
    public DateTime? EndedAt { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }
}
