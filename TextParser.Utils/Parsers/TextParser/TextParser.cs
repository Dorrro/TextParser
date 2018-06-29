namespace TextParser.Utils.Parsers.TextParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Extensions;

    public class TextParser
    {
        private static readonly Regex _Word = new Regex("[A-Za-z]+(?:'[A-Za-z]+)?");

        public static Text Parse(string text)
        {
            return new Text
                   {
                       Sentences = text?.Split(".", StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => s.Trim())
                           .Where(s => s != string.Empty)
                           .Select(SentenceObject)
                           .ToList() ?? new List<Sentence>()
                   };
        }

        private static Sentence SentenceObject(string sentence)
        {
            return new Sentence
                   {
                       Words = sentence
                           .FindMatches(_Word)
                           .Select(m => m.Value)
                           .OrderBy(w => w)
                           .ToList()
                   };
        }
    }
}