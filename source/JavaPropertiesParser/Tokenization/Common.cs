using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace JavaPropertiesParser.Tokenization
{
    public static class Common
    {
        public static readonly TextParser<TextSpan> UntilNewLineParser = Span
            .WithoutAny(c => c == '\r' || c == '\n');

        public static readonly TextParser<TextSpan> NewLineParser = Span
            .EqualTo("\n")
            .Or(Span.EqualTo("\r\n"));

        public static readonly TextParser<char> WhitespaceCharacterParser = Character
            .In(' ', '\t');

        public static readonly TextParser<Unit> UnicodeEscapeSequenceParser =
            from start in Span.EqualTo("\\u")
            from digits in Character.HexDigit.Repeat(4)
            select Unit.Value;
    }
}