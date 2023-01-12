namespace Mimbly.Application.Contracts.Dtos.MimboxLog;

using System;
using Mimbly.Domain.Entities;

public record MimboxLogDto
{
    public Guid Id { get; init; }

    public string Log { get; init; }

    public DateTime CreatedAt { get; init; }

    public Guid MimboxId { get; init; }

    public ICollection<MimboxLogImage> ImageList { get; init; }
}
