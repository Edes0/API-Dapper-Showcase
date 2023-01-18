namespace Mimbly.Application.Commands.MimboxLog.DeleteMimboxLog;

using MediatR;

public class DeleteMimboxLogCommand : IRequest
{
    public Guid Id { get; init; }
}