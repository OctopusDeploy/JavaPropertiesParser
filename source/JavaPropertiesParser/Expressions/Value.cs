using System.Linq;
using Superpower.Model;

namespace JavaPropertiesParser.Expressions
{
    public class Value : IExpression
    {
        public Value(params Token<TokenType>[] parts)
        {
            var contents = parts.Select(KeyComponents.GetStringValue);
            Content = StringValues.Join(contents);
        }

        public Value(string logicalValue)
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
    }
}