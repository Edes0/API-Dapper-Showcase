﻿namespace Mimbly.Domain.Entities.AzureEvents;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Error_Log")] // TODO: Change to Mimbox_Error_Model for future migration
public class ErrorLog
{
    [Key]
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Severity", TypeName = "nvarchar(50)")]
    public string Severity { get; set; }

    [Column("Message", TypeName = "nvarchar(max)")]
    public string Message { get; set; }

    [Column("Discarded", TypeName = "bit")]
    public bool Discarded { get; set; }

    [Column("Created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("Mimbox_Id", TypeName = "uniqueidentifier")]
    public Guid MimboxId { get; set; }
}
