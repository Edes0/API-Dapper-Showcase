namespace Mimbly.Application.Common.Validators;
using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class CreateMimboxRequestDtoValidator : AbstractValidator<CreateMimboxRequestDto>
{
    public CreateMimboxRequestDtoValidator()
    {
        RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
    }
}