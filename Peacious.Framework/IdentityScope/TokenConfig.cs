namespace Peacious.Framework.IdentityScope;

public class TokenConfig
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SecretKey { get; set; }
    public int ExpirationTimeInSec { get; set; }
    public int RefreshTokenExpirationTimeInSec { get; set; }
}