using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Separators
    {
        public static readonly Parser<SeparatorExpression> Parser =
            from leading in Parse.Chars(" \t").Many().Text()
            from text in Parse.Chars(":=").Optional()
            from trailing in Parse.Chars(" \t").Many().Text()
            select new SeparatorExpression(leading + (text.IsDefined ? text.Get().ToString() : "") + trailing);
    }
}