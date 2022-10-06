namespace Mimbly.Application.Common.Validators;
using Contracts.RequestDtos;
using FluentValidation;

public class CreateMimblyRequestDtoValidator : AbstractValidator<CreateMimboxRequestDto>
{
    public CreateMimblyRequestDtoValidator()
    {
        RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
    }
}