namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.ComponentModel.DataAnnotations;

public record CreateMimboxRequestDto
{
    [Required(ErrorMessage = "Nickname is required")]
    public string Nickname { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public Guid StatusId { get; init; }

    [Required(ErrorMessage = "Model is required")]
    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    public Guid? CompanyId { get; set; }
}