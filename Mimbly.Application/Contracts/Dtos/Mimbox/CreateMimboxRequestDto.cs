namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.ComponentModel.DataAnnotations;

public record CreateMimboxRequestDto
{
    [Required]
    public string Nickname { get; set; }

    [Required]
    public Guid StatusId { get; init; }

    [Required]
    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    [Required]
    public Guid CompanyId { get; set; }
}