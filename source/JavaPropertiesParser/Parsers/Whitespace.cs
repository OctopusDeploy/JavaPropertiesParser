using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Whitespace
    {
        public static readonly Parser<ITopLevelExpression> Parser =
            from text in Parse.Chars(" \t\r\n").XMany().Text()
            select new WhitespaceExpression(text);
    }
}