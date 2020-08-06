using System.Collections.Generic;
using System.Linq;
using System.Text;
using JavaPropertiesParser.Utils;

namespace JavaPropertiesParser.Expressions
{
    public class PropertiesDocument
    {
        public PropertiesDocument(IEnumerable<ITopLevelExpression> expressions)
        {
            Expressions = expressions.ToArray();
        }

        public PropertiesDocument(params ITopLevelExpression[] expressions)
        {
            Expressions = expressions.ToArray();
        }
        
        public IReadOnlyList<ITopLevelExpression> Expressions { get; }

        public PropertiesDocument Set(string key, string value)
        {
            var newExpressions = Expressions
                .Select(expr =>
                {
                    switch (expr)
                    {
                        case KeyValuePairExpression pair when pair.KeyExpression.Value.LogicalValue == key:
                            return new KeyValuePairExpression(
                                pair.KeyExpression,
                                pair.SeparatorExpression,
                                new ValueExpression(value)
                            );
                        
                        default:
                            return expr;
                    }
                });
            
            return new PropertiesDocument(newExpressions);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            
            foreach (var expr in Expressions)
            {
                result.Append(expr);
            }

            return result.ToString();
        }

        protected bool Equals(PropertiesDocument other)
        {
            return Expressions.SequenceEqual(other.Expressions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PropertiesDocument) obj);
        }

        public override int GetHashCode()
        {
            return Expressions.AggregateHashCode();
        }
    }
}