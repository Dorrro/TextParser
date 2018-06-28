namespace TextParser.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using FluentAssertions;
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
            var result = new TextParser().Parse(text);

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
            var result = new TextParser().Parse(text);

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
            var result = new TextParser().Parse(text);
            
            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData("     \n  ")]
        public void When_ParsingEmptyText_Should_ReturnEmptyTextObject(string text)
        {
            // arrange
            var expected = new Text();

            // act
            var result = new TextParser().Parse(text);

            // assert
            result.Should().BeEquivalentTo(expected);
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
            var result = new TextParser().Parse(text);
            
            // assert
            result.Should().BeEquivalentTo(expected);
        }
    }

    public class TextParser
    {
        private readonly Regex _word = new Regex("[A-Za-z]+(?:'[A-Za-z]+)?");

        public Text Parse(string text)
        {
            return new Text
                   {
                       Sentences = text.Split(".", StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => s.Trim())
                           .Where(s => s != string.Empty)
                           .Select(this.SentenceObject)
                           .ToList()
                   };
        }

        private Sentence SentenceObject(string sentence)
        {
            return new Sentence
                   {
                       Words = sentence
                           .FindMatches(this._word)
                           .Select(m => m.Value)
                           .OrderBy(w => w)
                           .ToList()
                   };
        }
    }

    public static class StringExtensions
    {
        public static IEnumerable<Match> FindMatches(this string text, Regex regex)
        {
            return regex.Matches(text);
        }
    }

    public class Sentence
    {
        public List<string> Words { get; set; }

        public Sentence()
        {
            this.Words = new List<string>();
        }
    }

    public class Text
    {
        public List<Sentence> Sentences { get; set; }

        public Text()
        {
            this.Sentences = new List<Sentence>();
        }
    }
}