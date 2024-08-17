using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Extensions;
using System.Security.Claims;

namespace Peacious.Framework.IdentityScope;

public class IdentityScopeContext(
    IHttpContextAccessor httpContextAccessor,
    IServiceScopeFactory serviceScopeFactory) : IIdentityScopeContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    private string? _currentToken;
    private List<Claim>? _claims;

    public string? Token
    {
        get
        {
            if (string.IsNullOrEmpty(_currentToken))
            {
                return _httpContextAccessor.HttpContext?.GetAccessToken();
            }

            return _currentToken;
        }
    }

    public List<Claim> Claims
    {
        get
        {
            _claims ??= TokenHelper.GetClaims(Token);
            return _claims;
        }
    }

    public Claim? GetClaim(string claimType)
    {
        return Claims.FirstOrDefault(claim => claim.Type == claimType);
    }

    public bool HasClaim(string claimType)
    {
        return GetClaim(claimType) is not null;
    }

    public IIdentityScopeContext Initiate(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Provided token is empty. Initiation skipped...");

            return this;
        }

        if (!string.IsNullOrEmpty(_currentToken))
        {
            throw new Exception($"Already have a token: {_currentToken}");
        }

        _currentToken = token;

        Console.WriteLine($"New IdentityScopeContext initiated with token : {_currentToken}");

        return this;
    }

    public IServiceScope InitiateNewScopeContext()
    {
        if (string.IsNullOrEmpty(Token))
        {
            Console.WriteLine($"Current Token is empty. So Creating new Scope without initiating the IdentityScopeContext");

            return _serviceScopeFactory.CreateScope();
        }

        return _serviceScopeFactory.InitiateNewIdentityScopeContext(Token);
    }
}