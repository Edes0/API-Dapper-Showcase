namespace Mimbly.Application.Contracts.Dtos.MimboxLog;

using System;
using System.ComponentModel.DataAnnotations;
using Mimbly.Domain.Entities;

public record CreateMimboxLogRequestDto
{
    public string? Log { get; init; }

    [Required(ErrorMessage = "Mimbox id is required")]
    public Guid MimboxId { get; init; }

    public ICollection<MimboxLogImage> ImageList { get; init; }
}
