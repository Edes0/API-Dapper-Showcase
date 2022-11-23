namespace Mimbly.Application.Common.Validators.Mimbox;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class UpdateMimboxRequestDtoValidator : AbstractValidator<UpdateMimboxRequestDto>
{
    public UpdateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.WaterSaved)
            .NotNull().WithMessage("Water is required");

        RuleFor(x => x.Co2Saved)
            .NotNull().WithMessage("Co2 is required");

        RuleFor(x => x.PlasticSaved)
            .NotNull().WithMessage("Plastic is required");

        RuleFor(x => x.EconomySaved)
            .NotNull().WithMessage("Economy is required");

        RuleFor(x => x.StatusId)
            .NotNull().WithMessage("Status id is required");

        RuleFor(x => x.ModelId)
            .NotNull().WithMessage("Model id is required");
    }
}