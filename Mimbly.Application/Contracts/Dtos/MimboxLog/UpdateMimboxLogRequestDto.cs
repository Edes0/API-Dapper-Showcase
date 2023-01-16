namespace Mimbly.Application.Contracts.Dtos.MimboxLog;

public record UpdateMimboxLogRequestDto
{
    public string? Log { get; init; }
}
