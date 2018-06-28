namespace TextParser.Utils.Parsers.TextParser
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Utils.Extensions;

    public class TextParser
    {
        private readonly Regex _word = new Regex("[A-Za-z]+(?:'[A-Za-z]+)?");

        public Text Parse(string text)
        {
            return new Text
                   {
                       Sentences = text.Split(".", StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => s.Trim())
                           .Where(s => s != string.Empty)
                           .Select(this.SentenceObject)
                           .ToList()
                   };
        }

        private Sentence SentenceObject(string sentence)
        {
            return new Sentence
                   {
                       Words = sentence
                           .FindMatches(this._word)
                           .Select(m => m.Value)
                           .OrderBy(w => w)
                           .ToList()
                   };
        }
    }
}