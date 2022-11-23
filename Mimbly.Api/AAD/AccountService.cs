namespace Mimbly.Api.AAD;

using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Mimbly.Api.AAD.DTOs;
using Mimbly.Application.Commands.Company.CreateCompany;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Domain.Entities;

public class AccountService : IAccountService
{
    private readonly IGraphService _graphService;
    private readonly ILogger<AccountService> _logger;
    private readonly IMediator _mediator;
    private readonly UriBuilder _redirectUrl = new("https://mimbly-frontend.azurewebsites.net");

    public AccountService(IGraphService graphService, ILogger<AccountService> logger, IMediator mediator)
    {
        _graphService = graphService;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<bool> InviteUser(UserInviteDTO user)
    {
        var redirectUrl = _redirectUrl.Path = "dashboard/" + user.GroupId;

        var userInvitation = GetInvitation(user, redirectUrl);
        var userInfo = GetUserInfo(user);

        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);
            AddMemberToGroup(user.GroupId, invitedUserId);

            return true;
        }

        return false;
    }

    public async Task<bool> InviteTechnician(UserInviteDTO technician)
    {
        var userInvitation = GetInvitation(technician, _redirectUrl.ToString());
        var userInfo = GetUserInfo(technician);

        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);

            return true;
        }

        return false;

        // TODO: Add technician to database, once entity is created
    }

    public async Task<bool> InviteAdmin(UserInviteDTO admin)
    {
        var userInvitation = GetInvitation(admin, _redirectUrl.ToString());
        var userInfo = GetUserInfo(admin);

        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);

            // TODO: Insert admin group id, best case have in memory dicitionary of companyName and Ids.
            AddMemberToGroup("Admin group Id", invitedUserId);

            return true;
        }

        return false;
    }

    public async Task<Company?> CreateCompany(UserInviteDTO owner, CompanyModel company)
    {
        var client = _graphService.GetClient();

        var redirectUrl = _redirectUrl.Path = "dashboard/" + owner.GroupId;

        var userInvitation = GetInvitation(owner, redirectUrl);
        var userInfo = GetUserInfo(owner);

        var groupInfo = new Group
        {
            DisplayName = company.Name,
            Description = company.Description,
        };

        var group = await client.Groups.Request().AddAsync(groupInfo);
        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);
            AddOwnerToGroup(group.Id, invitedUserId);

            var newCompany = await _mediator.Send(new CreateCompanyCommand { CreateCompanyRequest = new CreateCompanyRequestDto { Name = company.Name, ParentId = company.ParentId } });

            return newCompany;
        }

        return null;
    }

    public Task<bool> AddUserToCompany(UserInviteDTO user, Guid companyId) => throw new NotImplementedException();

    public static Invitation GetInvitation(UserInviteDTO user, string redirectUrl)
    {
        var invite = new Invitation
        {
            InvitedUserDisplayName = user.DisplayName,
            InvitedUserEmailAddress = user.EmailAddress,
            InviteRedirectUrl = redirectUrl
        };

        return invite;
    }

    public static User GetUserInfo(UserInviteDTO user)
    {
        var userInfo = new User
        {
            JobTitle = user.Contact?.JobTitle,
            MobilePhone = user.Contact?.MobilePhone,
            StreetAddress = user.Contact?.StreetAddress,
            City = user.Contact?.StreetAddress,
            Country = user.Contact?.Country
        };

        return userInfo;
    }

    public async Task<string?> InviteAndGetUserId(Invitation invite)
    {
        var client = _graphService.GetClient();

        try
        {
            var resp = await client.Invitations.Request().AddAsync(invite);
            var userId = resp.InvitedUser.Id;

            return userId;
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong inviting a user: ", ex);
            return null;
        }
    }

    public async void UpdateUserInfo(User userInfo, string userId)
    {
        var client = _graphService.GetClient();

        try
        {
            await client.Users[userId].Request().UpdateAsync(userInfo);
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong inviting a user: ", ex);
        }
    }

    public async void AddMemberToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        try
        {
            var dirObj = new DirectoryObject { Id = userId };
            await client.Groups[groupId].Members.References.Request().AddAsync(dirObj);
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong adding a member to a group: ", ex);
        }
    }

    public async void AddOwnerToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        try
        {
            var dirObj = new DirectoryObject { Id = userId };
            await client.Groups[groupId].Owners.References.Request().AddAsync(new DirectoryObject { Id = userId });
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong adding a member to a group: ", ex);
        }
    }


}
