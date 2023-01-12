namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.ComponentModel.DataAnnotations;

public record CreateMimboxRequestDto
{
    public float? WaterSaved { get; init; }

    public float? Co2Saved { get; init; }

    public float? PlasticSaved { get; init; }

    public float? EconomySaved { get; init; }

    public int? TotalTap { get; set; }

    public int? TotalWashes { get; set; }

    [Required(ErrorMessage = "Nickname is required")]
    public string Nickname { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public Guid StatusId { get; init; }

    [Required(ErrorMessage = "Model is required")]
    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    public Guid? CompanyId { get; set; }
}