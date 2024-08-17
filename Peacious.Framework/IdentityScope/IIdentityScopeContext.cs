using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Peacious.Framework.IdentityScope;

/// <summary>
/// Provides current scope (Token and Claims) if the user authenticated.
/// </summary>
public interface IIdentityScopeContext
{
    /// <summary>
    /// Current Token
    /// </summary>
    string? Token { get; }

    /// <summary>
    /// Current Claims parsed from Token
    /// </summary>
    List<Claim> Claims { get; }

    /// <summary>
    /// Initiator of a scope context. Whenever a scope starts it's have to invoke with the request token. It have to only use once or it will through exception.
    /// </summary>
    /// <param name="token">Identity Token</param>
    IIdentityScopeContext Initiate(string? token);

    /// <summary>
    /// Initiate a new Service scope and initiate with current token by default.
    /// </summary>
    /// <returns>a IServiceScope and that needs to dispose</returns>
    IServiceScope InitiateNewScopeContext();

    bool HasClaim(string claimType);
    Claim? GetClaim(string claimType);
}
