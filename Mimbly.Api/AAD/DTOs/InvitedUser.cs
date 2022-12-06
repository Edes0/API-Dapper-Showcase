namespace Mimbly.Api.AAD.DTOs;

using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Graph;
using Mimbly.CoreServices.Validation;

public class InvitedUser
{
    [EmailAddress]
    public string? EmailAddress { get; set; }

    public string? DisplayName { get; set; }

    public string? GroupId { get; set; }

    public string? JobTitle { get; set; }

    [Phone]
    public string? MobilePhone { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new InvitedUserValidator());
    }
}

internal class InvitedUserValidator : AbstractValidator<InvitedUser>
{
    public InvitedUserValidator()
    {
        RuleFor(user => user.EmailAddress).NotNull();
        RuleFor(user => user.DisplayName).NotNull();
    }
}

