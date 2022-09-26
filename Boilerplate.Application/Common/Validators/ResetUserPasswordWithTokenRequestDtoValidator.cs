namespace Boilerplate.Application.Common.Validators;

using Contracts.RequestDtos.Email;
using FluentValidation;

public class ResetUserPasswordWithTokenRequestDtoValidator : AbstractValidator<ResetUserPasswordWithTokenRequestDto>
{
    public ResetUserPasswordWithTokenRequestDtoValidator() => RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8).WithMessage("Please provide a valid password");
}