namespace TextParser.Utils.Parsers.TextParser
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Sentence
    {
        [XmlElement("word", Type = typeof(string))]
        public List<string> Words { get; set; }

        public Sentence()
        {
            this.Words = new List<string>();
        }
    }
}