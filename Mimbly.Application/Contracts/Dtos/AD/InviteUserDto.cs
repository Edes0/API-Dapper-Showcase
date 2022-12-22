namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;
using CoreServices.Validation;
using FluentValidation;

public class InviteUserDto
{

    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public Guid GroupId { get; set; }

    public string? Phone { get; set; }

    public string? Role { get; set; }

    public string? JobTitle { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UserInviteDtoValidator());
    }
}

internal class UserInviteDtoValidator : AbstractValidator<InviteUserDto>
{
    public UserInviteDtoValidator()
    {
        RuleFor(user => user.Email).NotEmpty().EmailAddress();
        RuleFor(user => user.FirstName).NotEmpty();
        RuleFor(user => user.GroupId).NotNull().NotEmpty();
    }
}