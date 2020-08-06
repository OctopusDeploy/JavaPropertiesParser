using System.Linq;
using Superpower.Model;

namespace JavaPropertiesParser.Expressions
{
    public class KeyExpression : IExpression
    {
        public KeyExpression(params Token<TokenType>[] parts)
        {
            var values = parts.Select(KeyComponents.GetStringValue);
            Value = StringValues.Join(values);
        }

        public StringValue Value { get; }

        public override string ToString()
        {
            return Value.SerializableValue;
        }

        protected bool Equals(KeyExpression other)
        {
            return Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KeyExpression) obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
    }
}