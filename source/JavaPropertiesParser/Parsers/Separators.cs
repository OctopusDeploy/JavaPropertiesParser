using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Separators
    {
        public static readonly Parser<SeparatorExpression> Parser =
            from leading in Parse.Chars(" \t").XMany().Text()
            from text in Parse.Chars(":=").XOptional()
            from trailing in Parse.Chars(" \t").XMany().Text()
            select new SeparatorExpression(leading + (text.IsDefined ? text.Get().ToString() : "") + trailing);
    }
}