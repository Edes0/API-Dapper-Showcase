namespace Mimbly.CoreServices.Authorization;

using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Class <c>GroupsAuthorizeAttribute</c> is part of the role based
/// authorization scheme for controllers.
/// </summary>
public class GroupsAuthorizeAttribute : AuthorizeAttribute
{
    const string POLICY_PREFIX = "Groups";

    public GroupsAuthorizeAttribute(string groups)
    {
        Groups = groups;
    }

    public string Groups {
        get
        {
            if (Policy?[POLICY_PREFIX.Length..] != string.Empty && Policy != null)
            {
                return Policy[POLICY_PREFIX.Length..];
            }
            return "";
        }
        set
        {
            Policy = $"{POLICY_PREFIX}{value}";
        }
    }
}
