using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using JavaPropertiesParser.Utils;

namespace JavaPropertiesParser.Expressions
{
    public class Document
    {
        public Document(params ITopLevelExpression[] expressions)
        {
            Expressions = expressions;
        }

        public ITopLevelExpression[] Expressions { get; }

        public Document SetValueIfExists(string key, string value)
        {
            var newExpressions = Expressions.Mutate(item =>
            {
                switch (item)
                {
                    case KeyValuePairExpression pair when (pair.Key?.Text?.LogicalValue ?? "") == key:
                        return new KeyValuePairExpression(
                            pair.Key,
                            pair.Separator,
                            new ValueExpression(
                                new StringValue(
                                    value,
                                    EncodeValue(value)
                                )
                            )
                        );

                    default:
                        return item;
                }
            });

            return ReferenceEquals(newExpressions, Expressions)
                ? this
                : new Document(newExpressions);
        }

        private string EncodeValue(string logicalValue)
        {
            var encodedValue = logicalValue
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");

            if (encodedValue.StartsWith(" "))
                encodedValue = "\\ " + encodedValue.Substring(1);

            var result = new StringBuilder();
            
            foreach (var ch in encodedValue)
            {
                if (IsIso88591Compatible(ch))
                {
                    result.Append(ch);
                }
                else
                {
                    result.Append(EscapeChar(ch));
                }
            }
            
            return result.ToString();
        }

        private static bool IsIso88591Compatible(char ch)
        {
            return ch >= 0x00 && ch < 0xFF;
        }
        
        private static string EscapeChar(char ch)
        {
            var hex = ((int)ch).ToString("x4");
            return $"\\u{hex}";
        }

        public override string ToString()
        {
            return string.Join("", Expressions.Select(e => e.ToString()));
        }

        protected bool Equals(Document other)
        {
            return Expressions.SequenceEqual(other.Expressions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Document) obj);
        }

        public override int GetHashCode()
        {
            return Expressions.AggregateHashCode();
        }
    }
}