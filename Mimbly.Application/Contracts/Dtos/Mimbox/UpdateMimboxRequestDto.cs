namespace Mimbly.Application.Contracts.Dtos.Mimbox;
using System.Threading.Tasks;
using Mimbly.Application.Common.Validators;
using Mimbly.CoreServices.Validation;
using Mimbly.Domain.Entities;

public record UpdateMimboxRequestDto
{
    public Guid Id { get; set; }

    public float Water { get; set; }

    public float Co2 { get; set; }

    public float Plastic { get; set; }

    public float Economy { get; set; }

    public MimboxStatus Status { get; set; }

    public MimboxModel Model { get; set; }

    public MimboxLocation? Location { get; set; }

    public Company? Company { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateMimboxRequestDtoValidator());
    }
}
