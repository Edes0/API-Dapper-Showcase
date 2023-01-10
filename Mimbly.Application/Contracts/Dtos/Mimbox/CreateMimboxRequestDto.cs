namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.Threading.Tasks;
using CoreServices.Validation;
using Mimbly.Application.Common.Validators.Mimbox;

public record CreateMimboxRequestDto
{
    public string Nickname { get; set; }

    public Guid StatusId { get; init; }

    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    public Guid? CompanyId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateMimboxRequestDtoValidator());
    }
}