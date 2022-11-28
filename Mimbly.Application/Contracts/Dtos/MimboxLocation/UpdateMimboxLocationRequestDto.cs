namespace Mimbly.Application.Contracts.Dtos.MimboxLocation;

using System.Threading.Tasks;
using Mimbly.Application.Common.Validators.MimboxLocation;
using Mimbly.CoreServices.Validation;

public record UpdateMimboxLocationRequestDto
{
    public string Country { get; init; }

    public string? Region { get; init; }

    public string? PostalCode { get; init; }

    public string City { get; init; }

    public string StreetAddress { get; init; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateMimboxLocationRequestDtoValidator());
    }
}
