namespace Mimbly.CoreServices.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

public class GroupsHandler : AuthorizationHandler<GroupsRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupsRequirement requirement)
    {
        var definedRoles = new Dictionary<string, string>();

        definedRoles.Add("Admin", "5c2ad723-4a10-4a75-9332-0d6fe48caaf9");
        definedRoles.Add("User", "e41e3672-5998-4b41-9417-c405eda461a8");
        definedRoles.Add("Technician", "2c960853-5c9e-47d6-a75e-eb649f39b07b");
        definedRoles.Add("CompanyAdmin", "ce2b2a21-8db7-4c7e-bb9c-9a73f309ee91");

        var userRoles = context.User.FindAll(ClaimTypes.Role).Select(item => item.Value);
        var rolesRequirements = requirement.Groups.Split(",").AsEnumerable();

        var translatedRolesRequirement = definedRoles
            .Where(role => rolesRequirements.Contains(role.Key))
            .Select(role => role.Value);

        if (translatedRolesRequirement.Count() != rolesRequirements.Count())
        {
            return Task.CompletedTask;
        }

        if (translatedRolesRequirement.All(value => userRoles.Contains(value)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
