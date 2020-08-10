using JavaPropertiesParser.Expressions;
using Sprache;

namespace JavaPropertiesParser.Parsers
{
    public static class KeyValuePairs
    {
        public static readonly Parser<ITopLevelExpression> Parser =
            from key in Keys.Parser.XOptional()
            from separator in Separators.Parser.XOptional()
            from value in Values.Parser.XOptional()
            select new KeyValuePairExpression(
                key.GetOrDefault(), 
                separator.GetOrDefault(), 
                value.GetOrDefault()
            );
    }
}