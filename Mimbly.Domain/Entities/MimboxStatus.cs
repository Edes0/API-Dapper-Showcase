namespace Mimbly.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Mimbox_Status")]
public class MimboxStatus
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; init; }

    [Column("Name", TypeName = "Nvarchar(50)")]
    public string Name { get; set; }

    public MimboxStatus(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public MimboxStatus()
    {
    }
}
