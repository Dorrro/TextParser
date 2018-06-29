namespace TextParser.Utils.Serializers
{
    using System.IO;
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
}