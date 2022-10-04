namespace Mimbly.Application.Common.Validators;

using Contracts.RequestDtos.Email;
using FluentValidation;

public class ResetUserPasswordRequestDtoValidator : AbstractValidator<ResetUserPasswordRequestDto>
{
    public ResetUserPasswordRequestDtoValidator() => RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Please provide a valid email");
}