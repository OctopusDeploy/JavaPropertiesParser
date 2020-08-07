using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Keys
    {
        public static readonly Parser<KeyExpression> Parser =
            from text in Parse.CharExcept(":= \t").AtLeastOnce().Text()
            select new KeyExpression(text);
    }
}