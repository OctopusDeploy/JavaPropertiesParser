using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Separators
    {
        public static readonly Parser<SeparatorExpression> Parser =
            from text in Parse.Chars(":= \t")
            select new SeparatorExpression(text.ToString());
    }
}