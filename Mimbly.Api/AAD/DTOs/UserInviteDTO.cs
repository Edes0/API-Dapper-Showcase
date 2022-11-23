namespace Mimbly.Api.AAD.DTOs;

using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Graph;
using Mimbly.CoreServices.Validation;

public class UserInviteDTO
{
    [EmailAddress]
    public string? EmailAddress { get; set; }

    public string? DisplayName { get; set; }

    public string? GroupId { get; set; }

    public Contact? Contact { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UserInviteValidator());
    }
}

public class Contact
{
    public string? JobTitle { get; set; }

    [Phone]
    public string? MobilePhone { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}

internal class UserInviteValidator : AbstractValidator<UserInviteDTO>
{
    public UserInviteValidator()
    {
        RuleFor(user => user.EmailAddress).NotNull();
        RuleFor(user => user.DisplayName).NotNull();
    }
}

