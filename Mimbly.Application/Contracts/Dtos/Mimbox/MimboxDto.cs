namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Mimbly.Domain.Entities;

public record MimboxDto
{
    public Guid Id { get; init; }

    public float WaterSaved { get; init; }

    public float Co2Saved { get; init; }

    public float PlasticSaved { get; init; }

    public float EconomySaved { get; init; }

    public string? Nickname { get; set; }

    public Guid StatusId { get; init; }

    public Guid ModelId { get; init; }

    public Guid? LocationId { get; init; }

    public Guid? CompanyId { get; set; }

    public ICollection<MimboxLog> MimboxLogList { get; init; }

    public MimboxStatus Status { get; init; }

    public MimboxModel Model { get; init; }

    public MimboxLocation? Location { get; init; }

    public Company? Company { get; set; }
}