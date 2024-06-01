using System.Security.Cryptography;
using System.Text;

namespace Peacious.Framework.Security;

public class CheckSumGenerator
{
    public static string GetCheckSum(string input)
    {
        using var md5 = MD5.Create();

        var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

        var hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        return hashString;
    }

    public static string GetCheckSum(params string[] inputs)
    {
        if (inputs.Length == 0) return string.Empty;

        Array.Sort(inputs);

        var stringBuilder = new StringBuilder();

        foreach (var input in inputs)
        {
            stringBuilder.Append(input);
        }

        return GetCheckSum(stringBuilder.ToString());
    }
}
