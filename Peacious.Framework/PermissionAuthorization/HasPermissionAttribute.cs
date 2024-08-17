using Microsoft.AspNetCore.Authorization;

namespace Peacious.Framework.PermissionAuthorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: permission) { }
}
