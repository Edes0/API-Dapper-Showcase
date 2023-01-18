namespace Mimbly.Application.Contracts.Dtos.MimboxModel;

using System.ComponentModel.DataAnnotations;

public record CreateMimboxModelRequestDto
{
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; init; }
}
