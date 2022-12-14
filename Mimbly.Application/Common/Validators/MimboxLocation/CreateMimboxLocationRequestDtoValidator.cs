namespace Mimbly.Application.Common.Validators.MimboxLocation;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class CreateMimboxLocationRequestDtoValidator : AbstractValidator<CreateMimboxLocationRequestDto>
{
    public CreateMimboxLocationRequestDtoValidator()
    {
        RuleFor(x => x.Country)
            .NotNull().WithMessage("Country is required");

        RuleFor(x => x.City)
            .NotNull().WithMessage("City is required");

        RuleFor(x => x.StreetAddress)
            .NotNull().WithMessage("Street address is required");
    }
}