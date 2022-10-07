namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System;

public record MimboxDto
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public int Age { get; init; }
}