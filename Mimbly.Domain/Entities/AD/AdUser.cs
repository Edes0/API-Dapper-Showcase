namespace Mimbly.Domain.Entities.AD;

using System.ComponentModel.DataAnnotations;

public class AdUser
{
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public string? JobTitle { get; set; }

    [Phone]
    public string? Phone { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}