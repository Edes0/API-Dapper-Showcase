namespace Mimbly.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Mimbox_Log")]
public class MimboxLog
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; init; }

    [Required]
    [Column("Log", TypeName = "Nvarchar(max)")]
    public string Log { get; set; }

    [Required]
    [Column("Created", TypeName = "Date")]
    public DateTime Created { get; set; } = DateTime.Now; //TODO: Change this and make it better, just did this to make it work

    [Required]
    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }

    //Navigation property

    //[ForeignKey("MimboxId")]
    public virtual Mimbox Mimbox { get; set; }


    public MimboxLog(string log)
    {
        Id = Guid.NewGuid();
        Log = log;
    }

    public MimboxLog()
    {
    }
}
