using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Separators
    {
        private static readonly Parser<string> DelimiterAndTrailingWhitespaceParser =
            from delimiter in Parse.Chars(":=")
            from trailing in Parse.Chars(" \t").XMany().Text()
            select delimiter + trailing;

        private static readonly Parser<string> DelimiterAndSurroundingWhitespaceParser =
            from leading in Parse.Chars(" \t").AtLeastOnce().Text()
            from rest in DelimiterAndTrailingWhitespaceParser.XOptional()
            select leading + rest.GetOrDefault();

        public static readonly Parser<SeparatorExpression> Parser =
            from text in DelimiterAndTrailingWhitespaceParser
                .XOr(DelimiterAndSurroundingWhitespaceParser)
            select new SeparatorExpression(text);
    }
}