namespace Mimbly.Application.Commands.Emails;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Interfaces;
using Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;
using Infrastructure.Security.Tokens.Interfaces;
using MediatR;
using Mimbly.Domain.Enitites;

public class InviteUsersCommandHandler : IRequestHandler<InviteUsersCommand, InvitedUserVm>
{
    private readonly IIdentityRepository _identityRepository;
    private readonly ITokenHandler _tokenHandler;
    private readonly IInviteUserService _inviteUserService;

    public InviteUsersCommandHandler(
        IIdentityRepository identityRepository,
        ITokenHandler tokenHandler,
        IInviteUserService inviteUserService)
    {
        _identityRepository = identityRepository;
        _tokenHandler = tokenHandler;
        _inviteUserService = inviteUserService;
    }

    private static User CreateNewUser(string email)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = GenerateRandomPassword(18),
            FirstName = null,
            LastName = null
        };
    }

    private static string GenerateRandomPassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var res = new StringBuilder();
        var rnd = new Random();

        while (0 < length--)
        {
            res.Append(validChars[rnd.Next(validChars.Length)]);
        }

        return res.ToString();
    }

    public Task<InvitedUserVm> Handle(InviteUsersCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}