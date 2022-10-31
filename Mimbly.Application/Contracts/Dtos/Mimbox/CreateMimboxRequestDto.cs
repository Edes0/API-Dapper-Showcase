namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.Threading.Tasks;
using Common.Validators;
using CoreServices.Validation;
using Mimbly.Domain.Entities;

public record CreateMimboxRequestDto
{
    public float Water { get; init; }

    public float Co2 { get; init; }

    public float Plastic { get; init; }

    public float Economy { get; init; }

    public MimboxStatus Status { get; init; }

    public MimboxModel Model { get; init; }

    public MimboxLocation? Location { get; init; }

    public Company? Company { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateMimboxRequestDtoValidator());
    }
}