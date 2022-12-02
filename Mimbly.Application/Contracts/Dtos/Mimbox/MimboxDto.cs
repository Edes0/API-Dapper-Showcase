namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System;
using Mimbly.Domain.Entities;
using Mimbly.Domain.Entities.AzureEvents;

public record MimboxDto
{
    public Guid Id { get; init; }

    public float WaterSaved { get; init; }

    public float Co2Saved { get; init; }

    public float PlasticSaved { get; init; }

    public float EconomySaved { get; init; }

    public string? Nickname { get; set; }

    public ICollection<MimboxLog> LogList { get; init; }

    public ICollection<MimboxErrorLog> ErrorLogList { get; init; }

    public MimboxStatus Status { get; init; }

    public MimboxModel Model { get; init; }

    public MimboxLocation? Location { get; init; }

    public Company? Company { get; set; }
}