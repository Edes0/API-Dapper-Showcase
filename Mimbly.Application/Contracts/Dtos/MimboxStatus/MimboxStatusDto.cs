namespace Mimbly.Application.Contracts.Dtos.MimboxStatus;

using System;

public record MimboxStatusDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}
