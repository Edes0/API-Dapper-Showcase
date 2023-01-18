namespace Mimbly.Application.Contracts.Dtos.MimboxStatus;

using System.ComponentModel.DataAnnotations;

public record CreateMimboxStatusRequestDto
{
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; init; }
}
