using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Utils;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Values
    {
        public static readonly Parser<StringValue> ValueCharsParser =
            from text in Parse.CharExcept("\r\n\\").XAtLeastOnce().Text()
            select new StringValue(text, text);

        private static readonly Parser<StringValue> ValueComponentParser = ValueCharsParser
            .XOr(Common.EscapeSequenceParser);

        public static readonly Parser<ValueExpression> Parser =
            from components in ValueComponentParser.XAtLeastOnce()
            select new ValueExpression(components.Join());
    }
}