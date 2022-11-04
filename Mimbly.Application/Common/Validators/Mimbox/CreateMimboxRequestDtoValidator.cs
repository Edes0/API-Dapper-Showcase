namespace Mimbly.Application.Common.Validators.Mimbox;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class CreateMimboxRequestDtoValidator : AbstractValidator<CreateMimboxRequestDto>
{
    public CreateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.Water)
            .NotNull().WithMessage("Water is required");

        RuleFor(x => x.Co2)
            .NotNull().WithMessage("Co2 is required");

        RuleFor(x => x.Plastic)
            .NotNull().WithMessage("Plastic is required");

        RuleFor(x => x.Economy)
            .NotNull().WithMessage("Economy is required");

        RuleFor(x => x.StatusId)
            .NotNull().WithMessage("Status is required");

        RuleFor(x => x.ModelId)
            .NotNull().WithMessage("Model is required");
    }
}