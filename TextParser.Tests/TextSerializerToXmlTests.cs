namespace TextParser.Tests
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using FluentAssertions;
    using Utils.Parsers.TextParser;
    using Xunit;

    public class TextSerializerToXmlTests
    {
        [Fact]
        public void When_SerializingSimpleSentenceToXml_Should_SerializeToXmlWithOnlyOneSentenceNodes()
        {
            // arrange
            var text = new Text
                       {
                           Sentences =
                           {
                               new Sentence
                               {
                                   Words =
                                   {
                                       "a",
                                       "had",
                                       "lamb",
                                       "little",
                                       "Mary"
                                   }
                               }
                           }
                       };
            var expectedXml = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<text>
  <sentence>
    <word>a</word>
    <word>had</word>
    <word>lamb</word>
    <word>little</word>
    <word>Mary</word>
  </sentence>
</text>";

            // act
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");
            var result = new TextSerializer().ToXml(text, xmlSerializerNamespaces);

            // assert
            result.Should()
                .Be(expectedXml);
        }

        [Fact]
        public void When_SerializingMultipleSentencesToXml_Should_SerializeToXmlWithMultipleSentenceNodes()
        {
            // arrange
            var text = new Text
                       {
                           Sentences =
                           {
                               new Sentence
                               {
                                   Words =
                                   {
                                       "a",
                                       "had",
                                       "lamb",
                                       "little",
                                       "Mary"
                                   }
                               },
                               new Sentence
                               {
                                   Words =
                                   {
                                       "Aesop",
                                       "and",
                                       "called",
                                       "came",
                                       "for",
                                       "Peter",
                                       "the",
                                       "wolf"
                                   }
                               },
                               new Sentence
                               {
                                   Words =
                                   {
                                       "Cinderella",
                                       "likes",
                                       "shoes"
                                   }
                               }
                           }
                       };
            var expectedXml = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<text>
  <sentence>
    <word>a</word>
    <word>had</word>
    <word>lamb</word>
    <word>little</word>
    <word>Mary</word>
  </sentence>
  <sentence>
    <word>Aesop</word>
    <word>and</word>
    <word>called</word>
    <word>came</word>
    <word>for</word>
    <word>Peter</word>
    <word>the</word>
    <word>wolf</word>
  </sentence>
  <sentence>
    <word>Cinderella</word>
    <word>likes</word>
    <word>shoes</word>
  </sentence>
</text>";

            // act
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");
            var result = new TextSerializer().ToXml(text, xmlSerializerNamespaces);

            // assert
            result.Should()
                .Be(expectedXml);
        }
    }

    public class TextSerializer
    {
        public string ToXml<T>(T objectToSerialize, XmlSerializerNamespaces xmlSerializerNamespaces)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stream = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings {Indent = true}))
                {
                    xmlWriter.WriteStartDocument(true);

                    xmlSerializer.Serialize(xmlWriter, objectToSerialize, xmlSerializerNamespaces);

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
