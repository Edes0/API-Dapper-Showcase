namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.ComponentModel.DataAnnotations;

public record CreateMimboxRequestDto
{
    [Required]
    [StringLength(55, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
    public string Nickname { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public Guid StatusId { get; init; }

    [Required(ErrorMessage = "Model is required")]
    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    public Guid? CompanyId { get; set; }
}