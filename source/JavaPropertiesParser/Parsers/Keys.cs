using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Utils;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Keys
    {
        private static readonly Parser<StringValue> KeyCharsParser =
            from chars in Parse.CharExcept(":= \t\\\r\n").XAtLeastOnce().Text()
            select new StringValue(chars, chars);

        private static readonly Parser<StringValue> KeyComponentParser = KeyCharsParser
            .XOr(Common.EscapeSequenceParser);

        public static readonly Parser<KeyExpression> Parser =
            from components in KeyComponentParser.XAtLeastOnce()
            select new KeyExpression(components.Join());
    }
}