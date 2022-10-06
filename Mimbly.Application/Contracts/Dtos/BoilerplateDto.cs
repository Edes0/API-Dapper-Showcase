namespace Mimbly.Application.Contracts.Dtos;

using System;

public class MimboxDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }
}