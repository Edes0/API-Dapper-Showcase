namespace Mimbly.Application.Commands.Emails;

using System.Text;
using Common.Interfaces;
using Domain.ApplicationModels.Email;
using Domain.DomainModels;
using Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;
using Infrastructure.Security.Tokens.Interfaces;
using MediatR;

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

    public async Task<InvitedUserVm> Handle(InviteUsersCommand request, CancellationToken cancellationToken)
    {
        var invitedUsers = request.InviteUsersRequestDto.Users.ToList();
        var existingUsers = invitedUsers
            .Select(async x => await _identityRepository.GetUserByEmail(x.Email))
            .Select(task => task.Result)
            .Where(user => user != null)
            .Select(user => new InvitedUserModel { Email = user.Email, Error = "Email already in use." })
            .ToList();

        var existingEmailAddressesForFiltering = existingUsers
            .Select(user => user.Email).ToList();

        var newEmailAddresses = invitedUsers
            .Where(user => !existingEmailAddressesForFiltering.Contains(user.Email)).ToList();

        var newUserModels = newEmailAddresses
            .Select(x => CreateNewUser(x.Email));

        await _identityRepository.CreateInvitedUsers(newUserModels);

        foreach (var invitedUser in newEmailAddresses)
        {
            var invitePasswordToken = _tokenHandler.BuildPasswordResetTokenForNewUser(invitedUser.Email);
            _inviteUserService.SendInviteMailToUser(invitedUser.Email, invitePasswordToken);
        }

        return new InvitedUserVm(existingUsers.Concat(newEmailAddresses));
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
}