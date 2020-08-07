using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Values
    {
        public static readonly Parser<ValueExpression> Parser =
            from text in Parse.CharExcept("\r\n").Many().Text()
            select new ValueExpression(text);
    }
}