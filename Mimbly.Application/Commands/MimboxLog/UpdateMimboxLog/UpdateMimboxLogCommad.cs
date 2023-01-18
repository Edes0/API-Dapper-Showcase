namespace Mimbly.Application.Commands.MimboxLog.UpdateMimboxLog;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxLog;

public class UpdateMimboxLogCommand : IRequest
{
    public UpdateMimboxLogRequestDto UpdateMimboxLogRequest { get; set; } = null!;
    public Guid Id { get; set; }
}
