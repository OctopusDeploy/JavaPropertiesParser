using System.Linq;
using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Documents
    {
        private static readonly Parser<ITopLevelExpression> ExpressionsParser = Comments.Parser
            .XOr(Whitespace.Parser)
            .XOr(KeyValuePairs.Parser);

        public static readonly Parser<Document> Parser =
            from expressions in ExpressionsParser.XMany()
            select new Document(expressions.ToArray());

    }
}