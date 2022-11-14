namespace Mimbly.CoreServices.Authorization;

using Microsoft.AspNetCore.Authorization;

public class GroupsRequirement : IAuthorizationRequirement
{
    public string Groups { get; set; }

    public GroupsRequirement(string groups)
    {
        Groups = groups;
    }
}
