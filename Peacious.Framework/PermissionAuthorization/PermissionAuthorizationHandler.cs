using Microsoft.AspNetCore.Authorization;

namespace Peacious.Framework.PermissionAuthorization;

public class PermissionAuthorizationHandler(
    IPermissionProvider permissionProvider) : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionProvider _permissionProvider = permissionProvider;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (await _permissionProvider.HasPermissionAsync(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
