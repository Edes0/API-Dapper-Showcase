namespace Mimbly.Application.Common.Validators.MimboxContact;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxContact;

public class UpdateMimboxContactRequestDtoValidator : AbstractValidator<UpdateMimboxContactRequestDto>
{
    public UpdateMimboxContactRequestDtoValidator()
    {
        RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required");

        RuleFor(x => x.FirstName)
               .NotNull().WithMessage("First name is required");

        RuleFor(x => x.LastName)
               .NotNull().WithMessage("Last name is required");

        RuleFor(x => x.Email)
               .NotNull().WithMessage("Email is required")
               .EmailAddress().WithMessage("Enter in the format: name@example.com");

        RuleFor(x => x.PhoneNumber)
               .NotNull().WithMessage("Phone number is required");

        RuleFor(x => x.MimboxId)
                .NotNull().WithMessage("Mimbox is required");
    }
}