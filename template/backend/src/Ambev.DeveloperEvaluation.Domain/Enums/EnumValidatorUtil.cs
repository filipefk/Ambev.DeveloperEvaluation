namespace Ambev.DeveloperEvaluation.Domain.Enums;

public class EnumValidatorUtil
{
    /// <summary>
    /// Validates if a given string can be parsed to a specified enum type.
    /// </summary>
    /// <typeparam name="T">The enum type to validate against.</typeparam>
    /// <param name="enumString">The string to validate.</param>
    /// <returns>True if the string can be parsed to the enum type; otherwise, false.</returns>
    public static bool BeAValidEnumString<T>(string enumString) where T : struct, Enum
    {
        return Enum.TryParse<T>(enumString.Trim(), true, out _);
    }

    /// <summary>
    /// Gets the names of the values of a specified enum type, separated by commas.
    /// </summary>
    /// <typeparam name="T">The enum type to get the names from.</typeparam>
    /// <param name="removeThis">The name to remove from the list of enum names.</param>
    /// <returns>A string containing the names of the enum values, separated by commas.</returns>
    public static string GetEnumNames<T>(string removeThis = null!) where T : Enum
    {
        var nameList = Enum.GetNames(typeof(T));

        if (!string.IsNullOrEmpty(removeThis))
        {
            nameList = nameList.Where(name => !name.Equals(removeThis, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        return string.Join(", ", nameList);
    }

    /// <summary>
    /// Converts a string to a specified enum type.
    /// </summary>
    /// <typeparam name="T">The enum type to convert to.</typeparam>
    /// <param name="enumString">The string to convert.</param>
    /// <returns>The enum value if the conversion is successful; otherwise, the default enum value.</returns>
    public static T ConvertToEnum<T>(string enumString) where T : struct, Enum
    {
        return Enum.TryParse<T>(enumString.Trim(), true, out var result) ? result : default;
    }
}


