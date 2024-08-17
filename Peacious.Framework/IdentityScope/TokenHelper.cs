using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Peacious.Framework.IdentityScope;

public static class TokenHelper
{
    public static string GenerateJwtToken(
        string issuer,
        string audience,
        string secretKey,
        int expiredTimeInSec,
        List<Claim>? claims = null,
        string securityAlgorithm = "")
    {
        if (string.IsNullOrEmpty(securityAlgorithm))
        {
            securityAlgorithm = SecurityAlgorithms.HmacSha256;
        }
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, securityAlgorithm);
            var expiredTime = DateTime.UtcNow.AddSeconds(expiredTimeInSec);
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiredTime,
                signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return string.Empty;
        }
    }

    public static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public static List<Claim> GetClaims(string? jwtToken)
    {
        try
        {
            jwtToken = GetPreparedToken(jwtToken);
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
            return jwtSecurityToken.Claims.ToList();
        }
        catch (Exception)
        {
            return new List<Claim>();
        }
    }

    public static bool TryValidateToken(string? jwtToken, TokenValidationParameters validationParameters, out string validationMessage)
    {
        try
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new Exception("JwtToken Empty.");
            }

            jwtToken = GetPreparedToken(jwtToken);

            new JwtSecurityTokenHandler()
                .ValidateToken(jwtToken, validationParameters, out var validatedToken);

            validationMessage = "Token Validated.";

            return true;
        }
        catch (Exception ex)
        {
            validationMessage = ex.Message;
            return false;
        }
    }

    public static bool IsExpired(string? jwtToken)
    {
        try
        {
            jwtToken = GetPreparedToken(jwtToken);
            var securityToken = new JwtSecurityToken(jwtToken);
            bool isExpired = securityToken.ValidTo < DateTime.UtcNow;
            return isExpired;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static string? GetPreparedToken(string? jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken) == false && jwtToken.StartsWith("Bearer "))
        {
            return jwtToken.Replace("Bearer ", "");
        }
        return jwtToken;
    }
}