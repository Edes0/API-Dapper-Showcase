namespace Mimbly.Application.Commands.Emails;

using Contracts.RequestDtos.Email;
using MediatR;

public class ResetUserPasswordByEmailCommand : IRequest
{
    public ResetUserPasswordByEmailCommand(ResetUserPasswordRequestDto resetUserPasswordRequest) => ResetUserPasswordRequest = resetUserPasswordRequest;

    public ResetUserPasswordRequestDto ResetUserPasswordRequest { get; set; }
}