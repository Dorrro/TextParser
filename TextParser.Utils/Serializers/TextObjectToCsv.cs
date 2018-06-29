namespace TextParser.Utils.Serializers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Extensions;
    using Parsers.TextParser;

    public class TextObjectToCsv : ISerializeTextObject
    {
        public string Serialize(Text text)
        {
            if (!text.Sentences.Any())
            {
                return string.Empty;
            }

            var maxNumberOfWords = text
                .Sentences
                .Select(s => s.Words.Count)
                .Max();
            var csvHeader = this.PrepareHeader(maxNumberOfWords);

            var sentences = text
                .Sentences
                .Select(CsvSentence)
                .Join(Environment.NewLine);
            return csvHeader + sentences;
        }

        private static string CsvSentence(Sentence sentence, int index)
        {
            return $"Sentence {index + 1}, " + CsvWords(sentence.Words);
        }

        private static string CsvWords(IEnumerable<string> words)
        {
            return words.Aggregate((result, w) => result + $", {w}");
        }

        private string PrepareHeader(int maxNumberOfWords)
        {
            return Enumerable
                .Range(1, maxNumberOfWords)
                .Aggregate(new StringBuilder(), (result, element) => result.Append($", Word {element}"))
                .Append(Environment.NewLine)
                .ToString();
        }
    }
}