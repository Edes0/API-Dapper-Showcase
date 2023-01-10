namespace Mimbly.Application.Contracts.Dtos.MimboxModel;

using System;

public record MimboxModelDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}
