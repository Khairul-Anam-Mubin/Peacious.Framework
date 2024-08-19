using System.Security.Claims;

namespace Peacious.Framework.IdentityScope;

public interface IIdentityContext
{
    /// <summary>
    /// Current Token
    /// </summary>
    string? Token { get; }

    /// <summary>
    /// Current Claims parsed from Token
    /// </summary>
    List<Claim> Claims { get; }

    bool HasClaim(string claimType);
    Claim? GetClaim(string claimType);
}
