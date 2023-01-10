namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;

public record InviteUserRequestDto
{
    [Required]
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public Guid GroupId { get; set; }

    public string? Phone { get; set; }

    public string? Role { get; set; }

    public string? JobTitle { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}