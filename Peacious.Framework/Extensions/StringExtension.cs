using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Peacious.Framework.Extensions;

public static class StringExtenstion
{
    public static T? Deserialize<T>(this string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return default;
        }

        try
        {
            var model = JsonSerializer.Deserialize<T>(str, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return model;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return default;
    }

    /// <summary>
    /// Converts the string to camelCase.
    /// </summary>
    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input) || !IsValidVariableString(input))
            return input;

        string pascalCase = input.ToPascalCase();
        return string.IsNullOrEmpty(pascalCase) ? input : char.ToLower(pascalCase[0], CultureInfo.InvariantCulture) + pascalCase.Substring(1);
    }

    /// <summary>
    /// Converts the string to PascalCase.
    /// </summary>
    public static string ToPascalCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input) || !IsValidVariableString(input))
            return input;

        string[] words = Regex.Split(input, @"[\s_]+");
        StringBuilder result = new StringBuilder();

        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                result.Append(char.ToUpper(word[0], CultureInfo.InvariantCulture));
                result.Append(word.Substring(1).ToLower(CultureInfo.InvariantCulture));
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Converts the string to snake_case.
    /// </summary>
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input) || !IsValidVariableString(input))
            return input;

        string normalized = Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2");
        normalized = Regex.Replace(normalized, @"[\s]+", "_").ToLowerInvariant();
        normalized = Regex.Replace(normalized, @"_{2,}", "_"); // Handle multiple underscores
        return normalized;
    }

    /// <summary>
    /// Checks if the input string is suitable for variable use case.
    /// </summary>
    private static bool IsValidVariableString(string input)
    {
        return Regex.IsMatch(input, @"^[a-zA-Z0-9\s_]+$");
    }

    public static string ToValidVariableName(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input string is null, empty, or whitespace and cannot be converted to a valid variable name.");

        // Trim leading and trailing white spaces
        input = input.Trim();

        // Replace multiple white spaces with a single space
        input = Regex.Replace(input, @"\s+", " ");

        // Remove all characters that are not letters, digits, or underscores
        string cleaned = Regex.Replace(input, @"[^a-zA-Z0-9_]", string.Empty);

        // Ensure the result is non-empty after cleaning
        if (string.IsNullOrEmpty(cleaned))
            throw new ArgumentException($"Input string '{input}' cannot be converted to a valid variable name.");

        // Throw an exception if the first character is not a letter or underscore
        if (!char.IsLetter(cleaned[0]) && cleaned[0] != '_')
            throw new ArgumentException($"Input string '{input}' starts with an invalid character '{cleaned[0]}', which is not allowed for a valid variable name.");

        // Ensure the first character is not a digit
        if (char.IsDigit(cleaned[0]))
            throw new ArgumentException($"Input string '{input}' starts with a number, which is not allowed for a valid variable name.");

        return cleaned;
    }

}