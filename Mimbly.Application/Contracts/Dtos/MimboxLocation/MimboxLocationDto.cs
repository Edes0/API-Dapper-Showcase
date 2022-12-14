namespace Mimbly.Application.Contracts.Dtos.MimboxLocation;

public record MimboxLocationDto
{
    public Guid Id { get; init; }

    public string Country { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string City { get; set; }

    public string StreetAddress { get; set; }
}
