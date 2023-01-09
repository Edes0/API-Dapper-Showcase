namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;
using Common.Validators.AD;
using CoreServices.Validation;
using FluentValidation;

public record InviteUserRequestDto
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
        await ValidatableEntity.ValidateEntityByFluentRules(this, new InviteUserRequestDtoValidator());
    }
}