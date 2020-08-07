using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Utils;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Keys
    {
        private static readonly Parser<StringValue> KeyCharsParser =
            from chars in Parse.CharExcept(":= \t\\\r\n").AtLeastOnce().Text()
            select new StringValue(chars, chars);

        private static readonly Parser<StringValue> KeyComponentParser = KeyCharsParser
            .Or(Common.EscapeSequenceParser);

        public static readonly Parser<KeyExpression> Parser =
            from components in KeyComponentParser.AtLeastOnce()
            select new KeyExpression(components.Join());
    }
}