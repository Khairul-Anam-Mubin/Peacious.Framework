using Microsoft.AspNetCore.Authorization;

namespace KCluster.Framework.Identity;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: permission) { }
}
