namespace Mimbly.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Mimbox_Log_Image")]
public class MimboxLogImage
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Url", TypeName = "Nvarchar(50)")]
    public string Url { get; set; }

    [Column("Mimbox_Log_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxLogId { get; set; }

    public MimboxLogImage(string url)
    {
        Id = Guid.NewGuid();
        Url = url;
    }

    public MimboxLogImage()
    {
    }
}