using Superpower;
using Superpower.Parsers;

namespace JavaPropertiesParser.Tokenization
{
    public static class Values
    {
        private static readonly TextParser<TokenType> EscapeSequenceParser =
            from slash in Character.EqualTo('\\')
            from escaped in Character.ExceptIn('\r', '\n', 'u')
            select TokenType.ValueEscapeSequence;

        private static readonly TextParser<TokenType> ValueCharParser = Span
            .WithAll(c => c != '\\' && c != '\r' && c != '\r')
            .Value(TokenType.ValueChars);

        private static readonly TextParser<TokenType> PhysicalNewLineParser =
            from slash in Character.EqualTo('\\')
            from newline in Common.NewLineParser
            from indentation in Common.WhitespaceCharacterParser.Many()
            select TokenType.ValuePhysicalNewLine;

        private static readonly TextParser<TokenType> UnicodeEscapeSequenceParser = Common
            .UnicodeEscapeSequenceParser
            .Value(TokenType.ValueUnicodeEscapeSequence);

        public static readonly TextParser<TokenType> Parser = ValueCharParser
            .Or(EscapeSequenceParser.Try())
            .Or(UnicodeEscapeSequenceParser.Try())
            .Or(PhysicalNewLineParser);
    }
}