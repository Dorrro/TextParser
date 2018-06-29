namespace TextParser.Utils.Parsers.TextParser
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("text", Namespace = "")]
    public class Text
    {
        [XmlElement("sentence", Type = typeof(Sentence))]
        public List<Sentence> Sentences { get; set; }

        public Text()
        {
            this.Sentences = new List<Sentence>();
        }
    }
}