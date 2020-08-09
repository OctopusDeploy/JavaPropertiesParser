using System;
using System.Collections.Generic;
using Assent;
using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Tests.TestUtils;
using JavaPropertiesParser.Utils;
using NUnit.Framework;

namespace JavaPropertiesParser.Tests
{
    public class DocumentFixture
    {
        [Test]
        public void CanReplaceValuesWhilePreservingFormatting()
        {
            var keyValues = new Dictionary<string, string>
            {
                { "KEY1", "The new value for key\n1" },
                { "key\\4", "The new value for key 4" },
                { "key5", "Key 5's\n\n\nnewvalue" },
                { "notarealkey", "Should not be found" },
                { "", "New value for the empty key" },
                { "key15", "" }
            };

            ITopLevelExpression Mutator(ITopLevelExpression expr)
            {
                switch (expr)
                {
                    case null:
                        throw new ArgumentNullException(nameof(expr));

                    case KeyValuePairExpression pair:
                        var key = pair.Key?.Text?.LogicalValue ?? "";
                        if (keyValues.TryGetValue(key, out var value))
                        {
                            return new KeyValuePairExpression(
                                pair.Key,
                                pair.Separator,
                                new ValueExpression(
                                    new StringValue(
                                        value,
                                        Encode.Value(value)
                                    )
                                )
                            );
                        }
                        else
                        {
                            return expr;
                        }

                    default:
                        return expr;
                }
            }
            
            var input = ResourceUtils.ReadEmbeddedResource("multiple-pairs-and-comments.properties");
            var parsed = Parser.Parse(input);
            var updated = parsed.Mutate(Mutator);

            var actual = updated.ToString();
            this.Assent(actual, TestEnvironment.AssentConfiguration);
        }
    }
}