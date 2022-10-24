namespace Mimbly.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mimbly.CoreServices.Enums;

[Table("Mimbox_Model")]
public class MimboxModel
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; init; }

    [Column("Name", TypeName = "Nvarchar(50)")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ModelType Name { get; set; }

    public MimboxModel(ModelType name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public MimboxModel()
    {
    }
}
