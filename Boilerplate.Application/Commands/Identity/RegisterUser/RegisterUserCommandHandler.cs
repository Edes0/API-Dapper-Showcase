namespace Boilerplate.Application.Commands.Identity.RegisterUser;

using Common.Interfaces;
using Infrastructure.Security;
using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IIdentityRepository _identityRepository;

    public RegisterUserCommandHandler(IIdentityRepository identityRepository) => _identityRepository = identityRepository;

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await request.RegisterUserRequestDto.Validate();

        var email = request.RegisterUserRequestDto.Email;
        var hashedPassword = PasswordHandler.HashPassword(request.RegisterUserRequestDto.Password);

        await _identityRepository.RegisterUser(email, hashedPassword, request.RegisterUserRequestDto.FirstName, request.RegisterUserRequestDto.LastName);

        await Task.Run(() => request.RegisterUserRequestDto.Validate(), cancellationToken);

        return Unit.Value;
    }
}