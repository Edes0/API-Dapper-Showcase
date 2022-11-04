namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.Threading.Tasks;
using Mimbly.Application.Common.Validators.Mimbox;
using Mimbly.CoreServices.Validation;

public record UpdateMimboxRequestDto
{
    public float Water { get; set; }

    public float Co2 { get; set; }

    public float Plastic { get; set; }

    public float Economy { get; set; }

    public Guid StatusId { get; set; }

    public Guid ModelId { get; set; }

    public Guid? LocationId { get; set; }

    public Guid? CompanyId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateMimboxRequestDtoValidator());
    }
}
