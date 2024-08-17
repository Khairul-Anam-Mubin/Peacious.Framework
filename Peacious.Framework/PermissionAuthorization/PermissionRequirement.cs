using Microsoft.AspNetCore.Authorization;

namespace Peacious.Framework.PermissionAuthorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}