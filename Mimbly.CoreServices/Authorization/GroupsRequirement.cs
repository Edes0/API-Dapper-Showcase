namespace Mimbly.CoreServices.Authorization;

using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Class <c>GroupsRequirement</c> is part of the role based
/// authorization scheme for controllers.
/// </summary>
public class GroupsRequirement : IAuthorizationRequirement
{
    public string Groups { get; set; }

    public GroupsRequirement(string groups)
    {
        Groups = groups;
    }
}
