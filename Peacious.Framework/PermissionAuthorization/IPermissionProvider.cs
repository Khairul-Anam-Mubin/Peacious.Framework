namespace Peacious.Framework.PermissionAuthorization;

public interface IPermissionProvider
{
    bool HasPermission(string permission);

    Task<bool> HasPermissionAsync(string permission);
}
