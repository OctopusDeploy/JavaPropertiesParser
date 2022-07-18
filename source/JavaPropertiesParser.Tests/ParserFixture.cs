using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;
using FluentAssertions;
using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Tests.TestUtils;
using NUnit.Framework;
using Sprache;
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

            var expected = new Document();

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
                BangComment(" This is a comment")
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
        public void CanParseAColonSeparator()
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
        public void CanParseASpaceSeparator()
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
        public void CanParseAnEqualsSeparator()
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

        [Test]
        public void CanParseAnEqualsSeparatorAndLeadingWhitespace()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-equals-separator-and-leading-whitespace.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator(" ="),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAnEqualsSeparatorWithTrailingWhitespace()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-equals-separator-and-trailing-whitespace.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator("= "),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseATabSeparator()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-tab-separator.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator("\t"),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseASeparatorWithMultipleWhitespaceCharacters()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-multi-whitespace-separator.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator(" 	"),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithALogicalNewLine()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-logical-newline.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key\n1", "key\\n1"),
                    Separator(":"),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithAPhysicalNewLine()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-physical-newline.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key", "ke\\\r\ny"),
                    Separator(":"),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithAPhysicalNewLineAndIndentation()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-physical-newline-and-indentation.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key", "ke\\\r\n   y"),
                    Separator(":"),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithAUnicodeEscape()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-unicode-escape.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("keyὅ", "key\\u1f45"),
                    Separator(":"),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithNeitherSeparatorNorValue()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-neither-separator-nor-value.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key", "key"),
                    null, //Separator(""),
                    null //Value("", "")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAKeyWithSeparatorButNoValue()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-separator-but-no-value.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key", "key"),
                    Separator(":"),
                    null //Value("", "")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAnEmptyKey()
        {
            var input = ResourceUtils.ReadEmbeddedResource("empty-key.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    null, //Key(""),
                    Separator(":"),
                    Value("value")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseWhitespaceBeforeAComment()
        {
            var input = ResourceUtils.ReadEmbeddedResource("whitespace-before-comment.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Whitespace("   "),
                HashComment(" This comment has leading whitespace")
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAValueWithAUnicodeEscape()
        {
            var input = ResourceUtils.ReadEmbeddedResource("value-with-unicode-escape.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator(":"),
                    Value("valũe", "val\\u0169e")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseAValueWithALogicalNewLine()
        {
            var input = ResourceUtils.ReadEmbeddedResource("value-with-logical-newline.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator(":"),
                    Value("val\r\nue", "val\\r\\nue")
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        [TestCase("value-with-physical-crlf-newline.properties")]
        [TestCase("value-with-physical-lf-newline.properties")]
        public void CanParseAValueWithAPhysicalNewLine(string resourceName)
        {
            var input = ResourceUtils.ReadEmbeddedResource(resourceName);
            var parsed = Parser.Parse(input);
            var expectedKey = Key("key");
            var expectedSeparator = Separator(":");

            var expectedValues = new List<ValueExpression>
            {
                Value("value", "val\\\r\nue"), // Windows newline - CRLF.
                Value("value", "val\\\nue") // Linux & MacOS newline - LF. 
            };

            parsed.Expressions.Should().ContainSingle().Which.Should().BeAssignableTo<KeyValuePairExpression>();
            var resultExpression = (KeyValuePairExpression)parsed.Expressions.First();
            resultExpression.Key.Should().Be(expectedKey);
            resultExpression.Separator.Should().Be(expectedSeparator);
            expectedValues.Should().Contain(resultExpression.Value);
        }

        [Test]
        [TestCase("value-with-physical-crlf-newline-and-indentation.properties")]
        [TestCase("value-with-physical-lf-newline-and-indentation.properties")]
        public void CanParseAValueWithAPhysicalNewLineAndIndentation(string resourceName)
        {
            var input = ResourceUtils.ReadEmbeddedResource(resourceName);
            var parsed = Parser.Parse(input);
            var expectedKey = Key("key");
            var expectedSeparator = Separator(":");

            var expectedValues = new List<ValueExpression>
            {
                Value("value", "val\\\r\n   ue"), // Windows newline - CRLF.
                Value("value", "val\\\n   ue") // Linux & MacOS newline - LF. 
            };

            parsed.Expressions.Should().ContainSingle().Which.Should().BeAssignableTo<KeyValuePairExpression>();
            var resultExpression = (KeyValuePairExpression)parsed.Expressions.First();
            resultExpression.Key.Should().Be(expectedKey);
            resultExpression.Separator.Should().Be(expectedSeparator);
            expectedValues.Should().Contain(resultExpression.Value);
        }

        [Test]
        public void ThrowsExceptionOnInvalidUnicodeEscape()
        {
            var input = ResourceUtils.ReadEmbeddedResource("invalid-unicode-escape.properties");

            Action action = () => Parser.Parse(input);
            action.Should()
                .ThrowExactly<ParseException>();
        }

        [Test]
        public void CanParseLikeJavaReference()
        {
            string UnixNewlines(string str) => str.Replace("\r", "");

            var inputPairs
                = Parser.Parse(ResourceUtils.ReadEmbeddedResource("parser-stress.properties"))
                    .Expressions
                    .OfType<KeyValuePairExpression>()
                    .ToDictionary(inputPair => UnixNewlines(inputPair.Key?.Text.LogicalValue ?? ""),
                        inputPair => UnixNewlines(inputPair.Value?.Text.LogicalValue ?? ""));
            var inputKeyDisplay = string.Join(Environment.NewLine, inputPairs.Select(pair => pair.Key));

            using var referenceReader
                = new StringReader(ResourceUtils.ReadEmbeddedResource("parser-stress-reference-interpretation.xml"));

            foreach (var referenceEntry in new XPathDocument(referenceReader)
                .CreateNavigator()
                .Select(@"//entry")
                .OfType<XPathNavigator>())
            {
                var referenceKey = UnixNewlines(referenceEntry.GetAttribute("key", ""));
                var referenceValue = UnixNewlines(referenceEntry.ToString());

                if (!inputPairs.TryGetValue(referenceKey, out var inputValue))
                    throw new Exception($"Expected to parse key '{referenceKey}', but only found: {inputKeyDisplay}");
                Assert.AreEqual(referenceValue, inputValue, $"Difference in value of '{referenceKey}'");
            }
        }

        [Test]
        public void CanParseASlashThenEofInKey()
        {
            var input = ResourceUtils.ReadEmbeddedResource("key-with-slash-eof.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key", "key\\"),
                    null,
                    null
                )
            );

            parsed.Should().Be(expected);
        }

        [Test]
        public void CanParseASlashThenEofInValue()
        {
            var input = ResourceUtils.ReadEmbeddedResource("value-with-slash-eof.properties");
            var parsed = Parser.Parse(input);

            var expected = Doc(
                Pair(
                    Key("key"),
                    Separator(":"),
                    Value("value", "value\\")
                )
            );

            parsed.Should().Be(expected);
        }
    }
}