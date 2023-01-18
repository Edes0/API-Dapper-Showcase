namespace Mimbly.Application.Contracts.Dtos.MimboxStatus;

using System.ComponentModel.DataAnnotations;

public record UpdateMimboxStatusRequestDto
{
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; init; }
}
