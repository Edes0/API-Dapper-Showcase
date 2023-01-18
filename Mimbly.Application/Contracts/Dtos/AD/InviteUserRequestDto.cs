namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;

public record InviteUserRequestDto
{
    [Required]
    [StringLength(55, MinimumLength = 2, ErrorMessage = "First name must be between {2} and {1} characters long.")]
    [RegularExpression(@"^[a-zA-Z''-'\s]*$",
        ErrorMessage = "First name contains invalid characters.")]
    public string FirstName { get; set; }
    public string? LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Email provided is not valid.")]
    public string Email { get; set; }

    [Required]
    public Guid GroupId { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    public string? JobTitle { get; set; }

    public string? Phone { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}