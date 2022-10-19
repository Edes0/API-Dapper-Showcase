namespace Mimbly.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mimbly.Domain.Enums;

[Table("Mimbox_Status")]
public class MimboxStatus
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; init; }

    [Column("Name", TypeName = "Nvarchar(50)")]
    public StatusType Name { get; set; }

    // Navigation property
    public virtual ICollection<Mimbox> Mimboxes { get; set; }


    public MimboxStatus(StatusType name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public MimboxStatus()
    {
    }

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MimboxStatus>(entity => entity.Property(x => x.Updated)
            .ValueGeneratedOnUpdate());
    }
}
