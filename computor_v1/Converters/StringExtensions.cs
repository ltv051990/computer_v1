using System.Text.RegularExpressions;

namespace computor_v1.Converters
{
    public static class StringExtensions
    {
        public static string ToStringWitoutSpaces(this string input)
            => Regex.Replace(input, @"\s+", "");
    }
}
