namespace Mimbly.Api.AAD.DTOs;

using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Mimbly.CoreServices.Validation;

public class UserInviteDTO
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public string? GroupId { get; set; }

    [Phone]
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

internal class UserInviteDtoValidator : AbstractValidator<UserInviteDTO>
{
    public UserInviteDtoValidator()
    {
        RuleFor(user => user.Email).NotNull();
        RuleFor(user => user.FirstName).NotNull();
        RuleFor(user => user.GroupId).NotNull();
    }
}
