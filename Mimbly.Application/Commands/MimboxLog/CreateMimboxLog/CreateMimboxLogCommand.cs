namespace Mimbly.Application.Commands.MimboxLog.CreateMimboxLog;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxLog;
using Mimbly.Domain.Entities;

public class CreateMimboxLogCommand : IRequest<MimboxLog>
{
    public CreateMimboxLogRequestDto CreateMimboxLogRequest { get; set; } = null!;
}