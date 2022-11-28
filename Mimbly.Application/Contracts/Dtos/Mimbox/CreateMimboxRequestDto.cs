namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.Threading.Tasks;
using CoreServices.Validation;
using Mimbly.Application.Common.Validators.Mimbox;

public record CreateMimboxRequestDto
{
    public float WaterSaved { get; init; }

    public float Co2Saved { get; init; }

    public float PlasticSaved { get; init; }

    public float EconomySaved { get; init; }

    public Guid StatusId { get; init; }

    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    public Guid? CompanyId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateMimboxRequestDtoValidator());
    }
}