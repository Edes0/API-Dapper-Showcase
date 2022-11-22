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

    [Column("Water_Condition", TypeName = "float")]
    public float? WaterCondition { get; set; }

    [Column("Pressure_From_Washing_Machine", TypeName = "float")]
    public float? PressureFromWashingMachine { get; set; }

    [Column("Filter_Clean", TypeName = "int")]
    public int? FilterClean { get; set; }

    [Column("Water_Temp_In", TypeName = "float")]
    public float? WaterTempIn { get; set; }

    public WaterColor? WaterColor { get; set; }

    //Navigation property
    public virtual Guid? WaterColorId { get; set; }
}
