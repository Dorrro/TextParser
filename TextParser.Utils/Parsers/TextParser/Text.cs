namespace TextParser.Utils.Parsers.TextParser
{
    using System.Collections.Generic;

    public class Text
    {
        public List<Sentence> Sentences { get; set; }

        public Text()
        {
            this.Sentences = new List<Sentence>();
        }
    }
}