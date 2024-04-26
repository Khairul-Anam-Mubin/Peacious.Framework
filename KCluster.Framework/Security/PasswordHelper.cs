using System.Security.Cryptography;

namespace KCluster.Framework.Security;

public class PasswordHelper
{
    public static string GenerateRandomSalt(int keySize)
    {
        return RandomNumberGenerator.GetHexString(keySize);
    }

    public static string GetPasswordHash(string password, string salt)
    {
        var passwordHash = CheckSumGenerator.GetCheckSum(password, salt);
        return passwordHash;
    }
}
