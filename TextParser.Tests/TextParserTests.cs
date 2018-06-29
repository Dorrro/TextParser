namespace TextParser.Tests
{
    using FluentAssertions;
    using Utils.Parsers.TextParser;
    using Xunit;

    public class TextParserTests
    {
        [Fact]
        public void When_SingleSentenceGiven_Should_SplitItIntoWordsAndSortAsc()
        {
            // arrange
            var text = "This is a simple sentence";
            var expected = new Text
                           {
                               Sentences =
                               {
                                   new Sentence
                                   {
                                       Words =
                                       {
                                           "a",
                                           "is",
                                           "sentence",
                                           "simple",
                                           "This"
                                       }
                                   }
                               }
                           };

            // act
            var result = TextParser.Parse(text);

            // assert
            result.Should()
                .BeEquivalentTo(expected);
        }

        [Fact]
        public void When_MultipleSentencesGiven_Should_SplitThemIntoSeparateSentenceObjects()
        {
            // arrange
            var text = "This is a more complex sentence. You should watch out.";
            var expected = new Text
                           {
                               Sentences =
                               {
                                   new Sentence
                                   {
                                       Words =
                                       {
                                           "a",
                                           "complex",
                                           "is",
                                           "more",
                                           "sentence",
                                           "This"
                                       }
                                   },
                                   new Sentence
                                   {
                                       Words =
                                       {
                                           "out",
                                           "should",
                                           "watch",
                                           "You"
                                       }
                                   }
                               }
                           };

            // act
            var result = TextParser
                .Parse(text);

            // assert
            result.Should()
                .BeEquivalentTo(expected);
        }

        [Fact]
        public void When_GivenTextWithMultipleWhiteSpaceses_Should_ExtractOnlyWordsFromTheSentence()
        {
            // arrange
            var text = @"  Mary   had a little  lamb  . 


            Peter called for the wolf, and Aesop came.

                Cinderella  likes shoes.";
            var expected = new Text
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

            // act
            var result = TextParser.Parse(text);

            // assert
            result.Should()
                .BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData("     \n  ")]
        public void When_ParsingEmptyText_Should_ReturnEmptyTextObject(string text)
        {
            // arrange
            var expected = new Text();

            // act
            var result = TextParser.Parse(text);

            // assert
            result.Should()
                .BeEquivalentTo(expected);
        }

        [Fact]
        public void When_AfterTheFinalPeriodThereAreSomeWhitespaces_Should_IgnoreThem()
        {
            // arrange
            var text = " . Sentence . ";
            var expected = new Text
                           {
                               Sentences =
                               {
                                   new Sentence
                                   {
                                       Words =
                                       {
                                           "Sentence"
                                       }
                                   }
                               }
                           };

            // act
            var result = TextParser.Parse(text);

            // assert
            result.Should()
                .BeEquivalentTo(expected);
        }
    }
}