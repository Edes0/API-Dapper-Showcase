namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;

public record AddCompanyRequestDto
{
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string Description { get; set; }

    [Required]
    public Guid ParentId { get; set; }
}