namespace Mimbly.Application.Contracts.Dtos.MimboxContact;

using System;

public record MimboxContactDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Guid MimboxId { get; set; }
}