namespace Mimbly.Application.Contracts.Dtos.MimboxLog;

using System;
using System.ComponentModel.DataAnnotations;

public record CreateMimboxLogRequestDto
{
    public string? Log { get; init; }

    [Required(ErrorMessage = "Mimbox id is required")]
    public Guid MimboxId { get; init; }
}
