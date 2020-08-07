using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class KeyValuePairs
    {
        public static readonly Parser<ITopLevelExpression> Parser =
            from key in Keys.Parser
            from separator in Separators.Parser
            from value in Values.Parser
            select new KeyValuePairExpression(key, separator, value);
    }
}