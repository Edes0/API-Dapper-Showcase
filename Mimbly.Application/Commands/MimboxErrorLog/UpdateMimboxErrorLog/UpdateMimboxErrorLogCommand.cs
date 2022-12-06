namespace Mimbly.Application.Commands.MimboxErrorLog.UpdateMimboxErrorLog;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxErrorLog;

public class UpdateMimboxErrorLogCommand : IRequest
{
    public Guid Id { get; set; }
    public UpdateMimboxErrorLogRequestDto UpdateMimboxErrorLogRequest { get; set; } = null!;
}