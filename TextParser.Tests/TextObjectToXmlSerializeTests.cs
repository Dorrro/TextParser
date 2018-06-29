namespace TextParser.Tests
{
    using System.Xml.Serialization;
    using FluentAssertions;
    using Utils.Parsers.TextParser;
    using Utils.Serializers;
    using Xunit;

    public class TextObjectToXmlSerializeTests
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
            var result = new TextObjectToXml().Serialize(text);

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
            var result = new TextObjectToXml().Serialize(text);

            // assert
            result.Should()
                .Be(expectedXml);
        }
    }
}
