namespace TextParser.Tests
{
    using FluentAssertions;
    using Utils.Parsers.TextParser;
    using Utils.Serializers;
    using Xunit;

    public class TextObjectToCsvSerializeTests
    {
        [Fact]
        public void When_SerializingSimpleSentence_Should_SerializeToOneLineSentenceAndTheAmountOfColumnsShouldEqualMaxNumberOfWords()
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
            var expectedCsv = @", Word 1, Word 2, Word 3, Word 4, Word 5
Sentence 1, a, had, lamb, little, Mary";

            // act
            var result = new TextObjectToCsv().Serialize(text);

            // assert
            result.Should()
                .Be(expectedCsv);
        }

        [Fact]
        public void When_SerializingMultipleSentencesToCsv_Should_SerializeSentencePerLine()
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
            var expectedCsv = @", Word 1, Word 2, Word 3, Word 4, Word 5, Word 6, Word 7, Word 8
Sentence 1, a, had, lamb, little, Mary
Sentence 2, Aesop, and, called, came, for, Peter, the, wolf
Sentence 3, Cinderella, likes, shoes";

            // act
            var result = new TextObjectToCsv().Serialize(text);

            // assert
            result.Should()
                .Be(expectedCsv);
        }

        [Fact]
        public void When_SerializingEmptyObjectToCsv_Should_ReturnEmptyString()
        {
            // arrange
            var text = new Text();
            var expectedCsv = @"";

            // act
            var result = new TextObjectToCsv().Serialize(text);

            // assert
            result.Should()
                .Be(expectedCsv);
        }
    }
}