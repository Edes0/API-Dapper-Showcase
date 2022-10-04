namespace Mimbly.Application.Commands.Identity.RefreshAccessToken;

using MediatR;

public class RefreshAccessTokenCommand : IRequest<RefreshAccessTokenVm>
{
    public string UserId { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}