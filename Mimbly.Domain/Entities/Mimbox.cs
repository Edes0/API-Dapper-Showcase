namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("Mimbox")]
public class Mimbox
{
    [Key] //TODO: Check if needed
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Water", TypeName = "float")]
    public float Water { get; set; } = 0;

    [Column("Co2", TypeName = "float")]
    public float Co2 { get; set; } = 0;

    [Column("Plastic", TypeName = "float")]
    public float Plastic { get; set; } = 0;

    [Column("Economy", TypeName = "float")]
    public float Economy { get; set; } = 0;

    [Column("Mimbox_Status_Id", TypeName = "uniqueidentifier")]
    public Guid StatusId { get; set; }

    [Column("Mimbox_Model_Id", TypeName = "uniqueidentifier")]
    public Guid ModelId { get; set; }

    [Column("Mimbox_Location_Id", TypeName = "uniqueidentifier")]
    public Guid? LocationId { get; set; }

    [Column("Parent_Id", TypeName = "uniqueidentifier")]
    public Guid? ParentId { get; set; }

    public Company ParentCompany { get; set; }

    public ICollection<Company> ChildCompanies { get; } = new List<Company>();

    public ICollection<MimboxLog>? MimboxLogs { get; } = new List<MimboxLog>();

    public virtual MimboxStatus Status { get; set; }
    public virtual MimboxModel Model { get; set; }

    public virtual Location? Location { get; set; }
  


    public Mimbox()
    {
        Id = Guid.NewGuid();
        MimboxLog log = new("Mimbox created"); //TODO: CHANGE
    }
}