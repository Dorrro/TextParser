namespace TextParser.Utils.Extensions
{
    using System.Collections.Generic;

    public static class IEnumerableExtensions
    {
        public static string Join(this IEnumerable<string> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }
    }
}