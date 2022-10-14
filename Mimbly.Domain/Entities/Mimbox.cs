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

    [Required]
    [Column("Water", TypeName = "float")]
    public float Water { get; set; } = 0;

    [Required]
    [Column("Carbon", TypeName = "float")]
    public float Carbon { get; set; } = 0;

    [Required]
    [Column("Plastic", TypeName = "float")]
    public float Plastic { get; set; } = 0;

    [Required]
    [Column("Economy", TypeName = "float")]
    public float Economy { get; set; } = 0;

    [Required]
    [Column("Mimbox_Status_Id", TypeName = "uniqueidentifier")]
    public Guid StatusId { get; set; }

    [Required]
    [Column("Mimbox_Model_Id", TypeName = "uniqueidentifier")]
    public Guid ModelId { get; set; }

    [Column("Mimbox_Location_Id", TypeName = "uniqueidentifier")]
    public Guid? LocationId { get; set; }

    [Column("Company_Id", TypeName = "uniqueidentifier")]
    public Guid? CompanyId { get; set; }


    //Navigation property
    public ICollection<MimboxLog>? MimboxLogs { get; set; }

    //[ForeignKey("StatusId")]
    public virtual MimboxStatus Status { get; set; }

    //[ForeignKey("ModelId")]
    public virtual MimboxModel Model { get; set; }

    //[ForeignKey("LocationId")]
    public virtual Location? Location { get; set; }

    //[ForeignKey("CompanyId")]
    public virtual Company? Company { get; set; }


    public Mimbox()
    {
        Id = Guid.NewGuid();
        MimboxLog log = new("Mimbox created"); //TODO: CHANGE
    }


    /**
    * Model configurations.
    *
    * @Param {ModelBuilder} modelBuilder - Used for entity configurations in database.
    */
}