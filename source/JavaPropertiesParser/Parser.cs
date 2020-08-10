using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Parsers;
using Sprache;

namespace JavaPropertiesParser
{
    public static class Parser
    {
        public static Document Parse(string input)
        {
            return Documents.Parser.End().Parse(input);
        }
    }
}