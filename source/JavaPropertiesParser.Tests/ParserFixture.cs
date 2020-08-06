using FluentAssertions;
using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Tests.TestUtils;
using NUnit.Framework;
using static JavaPropertiesParser.Build;

namespace JavaPropertiesParser.Tests
{
    public class ParserFixture
    {
        [Test]
        public void CanParseEmptyFile()
        {
            var input = ResourceUtils.ReadEmbeddedResource("empty.properties");
            var parsed = Parser.Parse(input);

            var expected = new PropertiesDocument();

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAHashComment()
        {
            var input = ResourceUtils.ReadEmbeddedResource("hash-comment.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                HashComment(" This is a comment")
            );
        
            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAnExclamationComment()
        {
            var input = ResourceUtils.ReadEmbeddedResource("exclamation-comment.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                ExclamationComment(" This is a comment")
            );
        
            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseWhitespace()
        {
            var input = ResourceUtils.ReadEmbeddedResource("blank-line.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Whitespace("\r\n")
            );
        
            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithAColonSeparatorAndValue()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-colon-separator.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator(":"),
                    Value("value")
                )
            );
        
            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithASpaceSeparatorAndValue()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-space-separator.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator(" "),
                    Value("value")
                )
            );
        
            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithAnEqualsSeparatorAndValue()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-equals-separator.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator("="),
                    Value("value")
                )
            );
        
            parsed.Should().Be(expected);
        }
    }
}