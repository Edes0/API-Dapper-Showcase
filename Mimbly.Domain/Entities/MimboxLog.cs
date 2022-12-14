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

    [Column("Log", TypeName = "Nvarchar(max)")]
    public string Log { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }

    public MimboxLog(string log)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Log = log;
    }

    public MimboxLog()
    {
    }
}
