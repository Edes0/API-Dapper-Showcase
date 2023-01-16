namespace Mimbly.Application.Contracts.Dtos.MimboxLogImage;

using System;
using System.ComponentModel.DataAnnotations;

public record CreateMimboxLogImageRequestDto
{
    [Required(ErrorMessage = "Image url is required")]
    public string Url { get; init; }

    [Required(ErrorMessage = "Mimbox log id is required")]
    public Guid MimboxLogId { get; init; }
}
