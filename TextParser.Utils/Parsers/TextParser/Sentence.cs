namespace TextParser.Utils.Parsers.TextParser
{
    using System.Collections.Generic;

    public class Sentence
    {
        public List<string> Words { get; set; }

        public Sentence()
        {
            this.Words = new List<string>();
        }
    }
}