namespace Mimbly.Application.Common.Validators.MimboxLocation;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class CreateMimboxLocationRequestDtoValidator : AbstractValidator<CreateMimboxLocationRequestDto>
{
    public CreateMimboxLocationRequestDtoValidator()
    {
        RuleFor(x => x.Country)
            .Empty().WithMessage("Country is required");

        RuleFor(x => x.City)
            .Empty().WithMessage("City is required");

        RuleFor(x => x.StreetAddress)
            .Empty().WithMessage("Street address is required");
    }
}