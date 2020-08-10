using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Comments
    {
        public static readonly Parser<ITopLevelExpression> Parser =
            from delimiter in Parse.Chars("!#")
            from text in Parse.CharExcept("\r\n").XMany().Text()
            select new CommentExpression(delimiter, text);
    }
}