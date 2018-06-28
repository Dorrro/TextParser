namespace TextParser.Utils.Extensions
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static IEnumerable<Match> FindMatches(this string text, Regex regex)
        {
            return regex.Matches(text);
        }
    }
}