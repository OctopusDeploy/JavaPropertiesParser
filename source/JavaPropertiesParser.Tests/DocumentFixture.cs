using Assent;
using JavaPropertiesParser.Tests.TestUtils;
using NUnit.Framework;

namespace JavaPropertiesParser.Tests
{
    public class DocumentFixture
    {
        [Test]
        public void CanReplaceValuesWhilePreservingFormatting()
        {
            var input = ResourceUtils.ReadEmbeddedResource("multiple-pairs-and-comments.properties");
            var parsed = Parser.Parse(input)
                .SetValueIfExists("KEY1", "The new value for key\n1")
                .SetValueIfExists("key\\4", "The new value for key 4")
                .SetValueIfExists("key5", "Key 5's\n\n\nnewvalue")
                .SetValueIfExists("notarealkey", "Should not be found")
                .SetValueIfExists("", "New value for the empty key")
                .SetValueIfExists("key15", "");
        
            var actual = parsed.ToString();
            this.Assent(actual, TestEnvironment.AssentConfiguration);
        }
    }
}