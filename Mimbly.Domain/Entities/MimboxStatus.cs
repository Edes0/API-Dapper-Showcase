namespace Mimbly.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mimbly.CoreServices.Enums;

[Table("Mimbox_Status")]
public class MimboxStatus
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; init; }

    [Column("Name", TypeName = "Nvarchar(50)")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusType Name { get; set; }

    public MimboxStatus(StatusType name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public MimboxStatus()
    {
    }
}
