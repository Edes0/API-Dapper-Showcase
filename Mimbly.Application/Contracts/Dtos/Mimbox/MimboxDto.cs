namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System;
using Mimbly.Domain.Entities;

public record MimboxDto
{
    public Guid Id { get; init; }

    public float Water { get; init; }

    public float Co2 { get; init; }

    public float Plastic { get; init; }

    public float Economy { get; init; }

    public Guid StatusId { get; init; }

    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    public Guid? CompanyId { get; set; }

    public ICollection<MimboxLog> MimboxLogList { get; init; }

    public MimboxStatus Status { get; init; }

    public MimboxModel Model { get; init; }

    public MimboxLocation? Location { get; init; }

    public Company? Company { get; init; }
}