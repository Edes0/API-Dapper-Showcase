﻿namespace Mimbly.Business.Helpers.AD;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Mimbly.Business.Interfaces.AD;
using Mimbly.Domain.Entities.AD;

public class AccountService : IAccountService
{
    private readonly IGraphService _graphService;
    private readonly ILogger<AccountService> _logger;
    private readonly IGraphHelper _graphHelper;
    private readonly IMemoryCache _memoryCache;
    private readonly string _redirectUrl = "https://mimbly-frontend.azurewebsites.net/dashboard/";

    public AccountService(IGraphService graphService, ILogger<AccountService> logger, IGraphHelper graphHelper, IMemoryCache memoryCache)
    {
        _graphService = graphService;
        _logger = logger;
        _graphHelper = graphHelper;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Method <c>InviteUser</c> invites a user to Azure Ad.
    /// </summary>
    /// <param name="user">A <c>AdUser</c> containing the information required to invite the user.</param>
    /// <returns>True if successful otherwise false.</returns>
    public async Task<bool> InviteUser(AdUser user)
    {
        var redirectUrl = _redirectUrl + user.GroupId;

        var userInvitation = _graphHelper.GetInvitation(user, redirectUrl);
        var userInfo = _graphHelper.GetUserInfo(user);

        var invitedUserId = await _graphHelper.InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            _graphHelper.UpdateUserInfo(userInfo, invitedUserId);
            _graphHelper.AddMemberToGroup(user.RoleId.ToString(), invitedUserId);
            _graphHelper.AddMemberToGroup(user.GroupId.ToString(), invitedUserId);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Method <c>CreateCompany</c> creates a group on Azure Ad with
    /// the provided information.
    /// </summary>
    /// <param name="company">A <c>AdCompany</c> containing the information required to create a group.</param>
    /// <returns>A Guid of the newly created <c>Group</c>.</returns>
    public async Task<string?> CreateCompany(AdCompany company)
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

    public Task<bool> AddUserToCompany(AdUser user, Guid companyId) => throw new NotImplementedException();

    /// <summary>
    /// Method <c>RemoveCompany</c> removes the specified
    /// group from Azure Ad.
    /// </summary>
    /// <param name="id">The group to be removed from Azure Ad.</param>
    /// <returns></returns>
    public Task RemoveCompany(Guid id)
    {
        var client = _graphService.GetClient();

        return client.Groups[id.ToString()].Request().DeleteAsync();
    }

    /// <summary>
    /// Method <c>GetRoles</c> gets all roles from Azure Ad.
    /// </summary>
    /// <returns>A list of <c>AdRole</c>.</returns>
    public async Task<List<AdRole>> GetRoles()
    {
        var selectedGroups = await _graphHelper.GetGroupsThatStartsWith("Role");

        var roles = new List<AdRole>();
        foreach (var group in selectedGroups)
        {
            var roleName = group.DisplayName.Split(" ")[2];
            var roleId = Guid.Parse(group.Id);

            roles.Add(new AdRole { RoleName = roleName, RoleId = roleId });
        }

        return roles;
    }
}
