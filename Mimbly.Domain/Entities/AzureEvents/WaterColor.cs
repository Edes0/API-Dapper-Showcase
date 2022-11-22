namespace Mimbly.Domain.Entities.AzureEvents;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Water_Color")]
public class WaterColor
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Red", TypeName = "float")]
    public float Red { get; set; }

    [Column("Green", TypeName = "float")]
    public float Green { get; set; }

    [Column("Blue", TypeName = "float")]
    public float Blue { get; set; }
}
