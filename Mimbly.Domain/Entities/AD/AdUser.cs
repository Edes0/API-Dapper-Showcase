namespace Mimbly.Domain.Entities.AD;

public class AdUser
{
    public string DisplayName { get; set; }

    public string Email { get; set; }

    public Guid GroupId { get; set; }

    public Guid RoleId { get; set; }

    public string? JobTitle { get; set; }

    public string? Phone { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}