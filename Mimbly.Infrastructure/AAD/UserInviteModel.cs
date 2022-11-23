namespace Mimbly.Infrastructure.AAD;

using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Graph;
using Mimbly.CoreServices.Validation;

public class UserInviteModel
{
    [EmailAddress]
    public string? EmailAddress { get; set; }

    public string? DisplayName { get; set; }

    public string? GroupId { get; set; }

    public Contact? Contact { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UserInviteModelValidator());
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

internal class UserInviteModelValidator : AbstractValidator<UserInviteModel>
{
    public UserInviteModelValidator()
    {
        RuleFor(user => user.EmailAddress).NotNull();
        RuleFor(user => user.DisplayName).NotNull();
    }
}

