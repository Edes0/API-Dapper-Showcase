namespace Mimbly.Application.Common.Validators;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class UpdateMimboxRequestDtoValidator : AbstractValidator<UpdateMimboxRequestDto>
{
    public UpdateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.Water)
            .NotNull().WithMessage("Water is required");

        RuleFor(x => x.Water)
            .NotNull().WithMessage("Water is required");

        RuleFor(x => x.Co2)
            .NotNull().WithMessage("Co2 is required");

        RuleFor(x => x.Plastic)
            .NotNull().WithMessage("Plastic is required");

        RuleFor(x => x.Economy)
            .NotNull().WithMessage("Economy is required");

        RuleFor(x => x.Status)
            .NotNull().WithMessage("Status is required");

        RuleFor(x => x.Model.Name)
            .IsInEnum().WithMessage("Valid status type is required");

        RuleFor(x => x.Model)
            .NotNull().WithMessage("Model is required");

        RuleFor(x => x.Model.Name)
            .IsInEnum().WithMessage("Valid model type is required");
    }
}