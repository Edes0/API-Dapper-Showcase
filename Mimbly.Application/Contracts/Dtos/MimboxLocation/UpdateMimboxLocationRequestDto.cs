namespace Mimbly.Application.Contracts.Dtos.MimboxLocation;

using System.ComponentModel.DataAnnotations;

public record UpdateMimboxLocationRequestDto
{
    [Required(ErrorMessage = "{0} is required")]
    public string Country { get; init; }

    public string? Region { get; init; }

    public string? PostalCode { get; init; }

    [Required(ErrorMessage = "{0} is required")]
    public string City { get; init; }

    [Required(ErrorMessage = "{0} is required")]
    public string StreetAddress { get; init; }
}
