using System.Linq;
using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class Documents
    {
        private static readonly Parser<ITopLevelExpression> ExpressionsParser = Comments.Parser
            .Or(Whitespace.Parser)
            .Or(KeyValuePairs.Parser);

        public static readonly Parser<Document> Parser =
            from expressions in ExpressionsParser.Many()
            select new Document(expressions.ToArray());

    }
}