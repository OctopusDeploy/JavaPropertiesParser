using FluentAssertions;
using JavaPropertiesParser.Tests.TestUtils;
using NUnit.Framework;

namespace JavaPropertiesParser.Tests
{
    public class RoundTripFixture
    {
        [Test]
        [TestCase("blank-line.properties")]
        [TestCase("empty-key.properties")]
        [TestCase("empty.properties")]
        [TestCase("exclamation-comment.properties")]
        [TestCase("hash-comment.properties")]
        [TestCase("key-with-colon-separator.properties")]
        [TestCase("key-with-equals-separator-and-leading-whitespace.properties")]
        [TestCase("key-with-equals-separator-and-trailing-whitespace.properties")]
        [TestCase("key-with-equals-separator.properties")]
        [TestCase("key-with-logical-newline.properties")]
        [TestCase("key-with-multi-whitespace-separator.properties")]
        [TestCase("key-with-neither-separator-nor-value.properties")]
        [TestCase("key-with-physical-newline-and-indentation.properties")]
        [TestCase("key-with-physical-newline.properties")]
        [TestCase("key-with-separator-but-no-value.properties")]
        [TestCase("key-with-space-separator.properties")]
        [TestCase("key-with-tab-separator.properties")]
        [TestCase("key-with-unicode-escape.properties")]
        [TestCase("multiple-pairs-and-comments.properties")]
        [TestCase("single-line.properties")]
        [TestCase("value-with-logical-newline.properties")]
        [TestCase("value-with-physical-newline-and-indentation.properties")]
        [TestCase("value-with-physical-newline.properties")]
        [TestCase("value-with-unicode-escape.properties")]
        [TestCase("whitespace-before-comment.properties")]
        [TestCase("whitespace-before-key.properties")]
        public void FileCanRoundTrip(string resourceName)
        {
            var text = ResourceUtils.ReadEmbeddedResource(resourceName);
            var parsed = Parser.Parse(text);
            var roundTripped = parsed.ToString();
            text.Should().Be(roundTripped);
        }
    }
}