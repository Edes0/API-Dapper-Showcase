namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;

public record AddCompanyRequestDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public Guid ParentId { get; set; }
}