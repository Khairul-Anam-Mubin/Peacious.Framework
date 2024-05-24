using Microsoft.AspNetCore.Authorization;

namespace Peacious.Framework.Identity;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: permission) { }
}
