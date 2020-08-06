using System.Linq;
using Superpower.Model;

namespace JavaPropertiesParser.Expressions
{
    public class ValueExpression : IExpression
    {
        public ValueExpression(params Token<TokenType>[] parts)
        {
            var contents = parts.Select(KeyComponents.GetStringValue);
            Content = StringValues.Join(contents);
        }

        public ValueExpression(string logicalValue)
        {
            // noship
            // TODO: unicode
            // TODO: leading spaces
            var serializableValue = logicalValue
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
            
            Content = new StringValue(serializableValue, logicalValue);
            
        }

        public StringValue Content { get; }

        public override string ToString()
        {
            return Content.SerializableValue;
        }

        protected bool Equals(ValueExpression other)
        {
            return Equals(Content, other.Content);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueExpression) obj);
        }

        public override int GetHashCode()
        {
            return Content != null ? Content.GetHashCode() : 0;
        }
    }
}