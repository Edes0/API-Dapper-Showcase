namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mimbly.Domain.Entities.AzureEvents;

[Table("Mimbox")]
public class Mimbox
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Water_Saved", TypeName = "float")]
    public float WaterSaved { get; set; }

    [Column("Co2_Saved", TypeName = "float")]
    public float Co2Saved { get; set; }

    [Column("Plastic_Saved", TypeName = "float")]
    public float PlasticSaved { get; set; }

    [Column("Economy_Saved", TypeName = "float")]
    public float EconomySaved { get; set; }

    [Column("Total_Tap", TypeName = "float")]
    public float TotalTap { get; set; }

    [Column("Total_Washes", TypeName = "int")]
    public int TotalWashes { get; set; }

    [Column("Nickname", TypeName = "Nvarchar(50)")]
    public string Nickname { get; set; }

    [Column("Mimbox_Status_Id", TypeName = "uniqueidentifier")]
    public Guid StatusId { get; set; }

    [Column("Mimbox_Model_Id", TypeName = "uniqueidentifier")]
    public Guid ModelId { get; set; }

    [Column("Mimbox_Location_Id", TypeName = "uniqueidentifier")]
    public Guid? LocationId { get; set; }

    [Column("Company_Id", TypeName = "uniqueidentifier")]
    public Guid? CompanyId { get; set; }

    [Column("Stats_Updated_At", TypeName = "datetime")]
    public DateTime StatsUpdatedAt { get; set; }

    public ICollection<MimboxLog> LogList { get; set; } = new List<MimboxLog>();

    public ICollection<MimboxContact> ContactList { get; set; } = new List<MimboxContact>();

    public ICollection<MimboxErrorLog> ErrorLogList { get; set; } = new List<MimboxErrorLog>();

    public MimboxStatus Status { get; set; }

    public MimboxModel Model { get; set; }

    public MimboxLocation? Location { get; set; }

    public Company? Company { get; set; }

    // Navigation property
    public virtual ICollection<EventLog> EventLogList { get; set; }

    public virtual ICollection<WashStats> WaterToWashingMachineEventList { get; set; }

    public Mimbox()
    {
        Id = Guid.NewGuid();
        WaterSaved = 0;
        Co2Saved = 0;
        PlasticSaved = 0;
        EconomySaved = 0;
        TotalTap = 0;
        TotalWashes = 0;
        StatsUpdatedAt = DateTime.Now;
    }
}