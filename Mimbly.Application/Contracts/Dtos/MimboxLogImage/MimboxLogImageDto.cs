namespace Mimbly.Application.Contracts.Dtos.MimboxLogImage;

using System;

public record MimboxLogImageDto
{
    public Guid Id { get; init; }

    public string Url { get; init; }
}
