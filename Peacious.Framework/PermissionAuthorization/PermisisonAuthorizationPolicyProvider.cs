﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Peacious.Framework.PermissionAuthorization;

public class PermisisonAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermisisonAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    { }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}
