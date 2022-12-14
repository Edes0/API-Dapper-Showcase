namespace Mimbly.Application.Common.Validators.MimboxLocation;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class UpdateMimboxLocationRequestDtoValidator : AbstractValidator<UpdateMimboxLocationRequestDto>
{
    public UpdateMimboxLocationRequestDtoValidator()
    {
        RuleFor(x => x.Country)
            .NotNull().WithMessage("Country is required");

        RuleFor(x => x.City)
            .NotNull().WithMessage("City is required");

        RuleFor(x => x.StreetAddress)
            .NotNull().WithMessage("Street address is required");
    }
}