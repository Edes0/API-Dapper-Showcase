namespace Mimbly.Application.Common.Validators.AD;

using Contracts.Dtos.AD;
using FluentValidation;

public class InviteUserRequestDtoValidator : AbstractValidator<InviteUserRequestDto>
{
    public InviteUserRequestDtoValidator()
    {
        RuleFor(user => user.Email).NotEmpty().EmailAddress();
        RuleFor(user => user.FirstName).NotEmpty();
        RuleFor(user => user.GroupId).NotEmpty();
    }
}