namespace Mimbly.CoreServices.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

/// <summary>
/// Class <c>GroupsPolicyProvider</c> is part of the role based
/// authorization scheme for controllers.
/// </summary>
public class GroupsPolicyProvider : IAuthorizationPolicyProvider
{
    const string POLICY_PREFIX = "Groups";
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public GroupsPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    // Fallback policies.
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

    // Our policy.
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
        {
            var groups = policyName[POLICY_PREFIX.Length..];
            var policy = new AuthorizationPolicyBuilder();

            policy.AddRequirements(new GroupsRequirement(groups));

            return Task.FromResult(policy?.Build());
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}
