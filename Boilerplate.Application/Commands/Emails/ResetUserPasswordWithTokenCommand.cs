namespace Boilerplate.Application.Commands.Emails;

using Contracts.RequestDtos.Email;
using MediatR;

public class ResetUserPasswordWithTokenCommand : IRequest
{
    public ResetUserPasswordWithTokenCommand(ResetUserPasswordWithTokenRequestDto resetUserPasswordWithTokenRequest) => ResetUserPasswordWithTokenRequest = resetUserPasswordWithTokenRequest;

    public ResetUserPasswordWithTokenRequestDto ResetUserPasswordWithTokenRequest { get; set; }
}