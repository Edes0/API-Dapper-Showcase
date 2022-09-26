namespace Boilerplate.Application.Commands.Identity.LogoutUserCurrent;

using Common.Interfaces;
using MediatR;

public class LogoutUserCurrentCommandHandler : IRequestHandler<LogoutUserCurrentCommand>
{
    private readonly IIdentityRepository _identityRepository;

    public LogoutUserCurrentCommandHandler(IIdentityRepository identityRepository) => _identityRepository = identityRepository;

    public async Task<Unit> Handle(LogoutUserCurrentCommand request, CancellationToken cancellationToken)
    {
        await _identityRepository.LogoutUserCurrentDevice(request.UserId, request.RefreshToken);

        return Unit.Value;
    }
}