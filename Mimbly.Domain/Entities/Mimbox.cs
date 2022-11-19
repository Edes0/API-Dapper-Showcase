namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Mimbox")]
public class Mimbox
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Water", TypeName = "float")]
    public float WaterSaved { get; set; } = 0;

    [Column("Co2", TypeName = "float")]
    public float Co2Saved { get; set; } = 0;

    [Column("Plastic", TypeName = "float")]
    public float PlasticSaved { get; set; } = 0;

    [Column("Economy", TypeName = "float")]
    public float EconomySaved { get; set; } = 0;

    [Column("Mimbox_Status_Id", TypeName = "uniqueidentifier")]
    public Guid StatusId { get; set; }

    [Column("Mimbox_Model_Id", TypeName = "uniqueidentifier")]
    public Guid ModelId { get; set; }

    [Column("Mimbox_Location_Id", TypeName = "uniqueidentifier")]
    public Guid? LocationId { get; set; }

    [Column("Company_Id", TypeName = "uniqueidentifier")]
    public Guid? CompanyId { get; set; }

    public ICollection<MimboxLog> MimboxLogList { get; set; } = new List<MimboxLog>();

    public ICollection<MimboxContact> ContactList { get; set; } = new List<MimboxContact>();

    public ICollection<ErrorLog> ErrorLogList { get; set; } = new List<ErrorLog>();

    public MimboxStatus Status { get; set; }

    public MimboxModel Model { get; set; }

    public MimboxLocation? Location { get; set; }

    // Navigation property
    public virtual ICollection<EventLog> EventLogList { get; set; }

    public virtual ICollection<WaterToWashingMachineEvent> WaterToWashingMachineEventList { get; set; }

    public virtual ICollection<WaterToMimboxEvent> WaterToMimboxEventList { get; set; }


    public Mimbox()
    {
        Id = Guid.NewGuid();
        WaterSaved = 0;
    }
}