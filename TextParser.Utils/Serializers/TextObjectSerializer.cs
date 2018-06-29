namespace TextParser.Utils.Serializers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Parsers.TextParser;

    public class TextObjectToXml : ISerializeTextObject
    {
        public string Serialize(Text text)
        {
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");

            var xmlSerializer = new XmlSerializer(typeof(Text));
            using (var stream = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings {Indent = true}))
                {
                    xmlWriter.WriteStartDocument(true);

                    xmlSerializer.Serialize(xmlWriter, text, xmlSerializerNamespaces);

                    return stream.ToString();
                }
            }
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }

    public class TextObjectToCsv : ISerializeTextObject
    {
        public string Serialize(Text text)
        {
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

    public interface ISerializeTextObject
    {
        string Serialize(Text text);
    }

    public static class IEnumerableExtensions
    {
        public static string Join(this IEnumerable<string> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }
    }
}