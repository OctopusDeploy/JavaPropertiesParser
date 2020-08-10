using Sprache;

namespace JavaPropertiesParser.Utils
{
    public static class ParserExtensions
    {
        public static Parser<TOut> Cast<TIn, TOut>(this Parser<TIn> source) where TOut : TIn
        {
            return source.Select(v => (TOut) v);
        }
    }
}