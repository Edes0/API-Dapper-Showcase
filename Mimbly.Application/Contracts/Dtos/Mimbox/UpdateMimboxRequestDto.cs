namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.Threading.Tasks;
using Mimbly.Application.Common.Validators.Mimbox;
using Mimbly.CoreServices.Validation;

public record UpdateMimboxRequestDto
{
    public float WaterSaved { get; set; }

    public float Co2Saved { get; set; }

    public float PlasticSaved { get; set; }

    public float EconomySaved { get; set; }

    public string? Nickname { get; set; }

    public Guid StatusId { get; set; }

    public Guid ModelId { get; set; }

    public Guid? LocationId { get; set; }

    public Guid? CompanyId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateMimboxRequestDtoValidator());
    }
}
