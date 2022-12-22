namespace Mimbly.Api.AAD;

using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Mimbly.Api.AAD.DTOs;
using Mimbly.Api.AAD.Helpers;
using Mimbly.Application.Commands.Company.CreateCompany;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Domain.Entities;

public class AccountService : IAccountService
{
    private readonly IGraphService _graphService;
    private readonly ILogger<AccountService> _logger;
    private readonly IGraphHelper _graphHelper;
    private readonly string _redirectUrl = "https://mimbly-frontend.azurewebsites.net/dashboard/";

    public AccountService(IGraphService graphService, ILogger<AccountService> logger, IGraphHelper graphHelper)
    {
        _graphService = graphService;
        _logger = logger;
        _graphHelper = graphHelper;
    }

    public async Task<bool> InviteUser(InvitedUser user)
    {
        var redirectUrl = _redirectUrl + user.GroupId;

        var userInvitation = _graphHelper.GetInvitation(user, redirectUrl);
        var userInfo = _graphHelper.GetUserInfo(user);

        var invitedUserId = await _graphHelper.InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            _graphHelper.UpdateUserInfo(userInfo, invitedUserId);
            _graphHelper.AddMemberToGroup(user.GroupId, invitedUserId);

            return true;
        }

        return false;
    }

    public async Task<bool> InviteTechnician(InvitedUser technician)
    {
        var userInvitation = _graphHelper.GetInvitation(technician, _redirectUrl);
        var userInfo = _graphHelper.GetUserInfo(technician);

        var invitedUserId = await _graphHelper.InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            _graphHelper.UpdateUserInfo(userInfo, invitedUserId);

            return true;
        }

        return false;

        // TODO: Add technician to database, once entity is created
    }

    public async Task<bool> InviteAdmin(InvitedUser admin)
    {
        var userInvitation = _graphHelper.GetInvitation(admin, _redirectUrl);
        var userInfo = _graphHelper.GetUserInfo(admin);

        var invitedUserId = await _graphHelper.InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            _graphHelper.UpdateUserInfo(userInfo, invitedUserId);

            // TODO: Insert admin group id, best case have in memory dicitionary of companyName and Ids.
            _graphHelper.AddMemberToGroup("Admin group Id", invitedUserId);

            return true;
        }

        return false;
    }

    public async Task<string?> CreateCompany(CompanyModel company)
    {
        var client = _graphService.GetClient();

        var groupInfo = new Group
        {
            DisplayName = company.Name,
            Description = company.Description,
            GroupTypes = new List<string>() { },
            MailEnabled = false,
            MailNickname = "mimbly",
            SecurityEnabled = true,
            AdditionalData = new Dictionary<string, object>() { }
        };

        var group = await client.Groups.Request().AddAsync(groupInfo);
        return group.Id ?? null;
    }

    public Task<bool> AddUserToCompany(InvitedUser user, Guid companyId) => throw new NotImplementedException();
}
