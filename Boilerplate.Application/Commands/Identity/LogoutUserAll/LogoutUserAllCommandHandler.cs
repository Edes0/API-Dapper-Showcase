namespace Boilerplate.Application.Commands.Identity.LogoutUserAll;

using Common.Interfaces;
using MediatR;

public class LogoutUserAllCommandHandler : IRequestHandler<LogoutUserAllCommand>
{
    private readonly IIdentityRepository _identityRepository;

    public LogoutUserAllCommandHandler(IIdentityRepository identityRepository) => _identityRepository = identityRepository;

    public async Task<Unit> Handle(LogoutUserAllCommand request, CancellationToken cancellationToken)
    {
        await _identityRepository.LogoutUserAllDevices(request.UserId);

        return Unit.Value;
    }
}