namespace Mimbly.Application.Common.Validators.Mimbox;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class UpdateMimboxRequestDtoValidator : AbstractValidator<UpdateMimboxRequestDto>
{
    public UpdateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.WaterSaved)
            .NotEmpty().WithMessage("Water is required");

        RuleFor(x => x.Co2Saved)
            .NotEmpty().WithMessage("Co2 is required");

        RuleFor(x => x.PlasticSaved)
            .NotEmpty().WithMessage("Plastic is required");

        RuleFor(x => x.EconomySaved)
            .NotEmpty().WithMessage("Economy is required");

        RuleFor(x => x.TotalTap)
            .NotEmpty().WithMessage("Total Tap is required");

        RuleFor(x => x.TotalWashes)
            .NotEmpty().WithMessage("Total Washes is required");

        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("Status id is required");

        RuleFor(x => x.ModelId)
            .NotEmpty().WithMessage("Model id is required");

        RuleFor(x => x.Nickname)
            .Length(1, 50).WithMessage("Nickname length is invalid");
    }
}