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

    [Column("Company_Id", TypeName = "uniqueidentifier")]
    public Guid? CompanyId { get; set; }

    public ICollection<MimboxLog> MimboxLogList { get; set; } = new List<MimboxLog>();

    public ICollection<MimboxContact> ContactList { get; set; } = new List<MimboxContact>();

    public MimboxStatus Status { get; set; }

    public MimboxModel Model { get; set; }

    public MimboxLocation? Location { get; set; }


    public Mimbox()
    {
        Id = Guid.NewGuid();
    }
}