using Superpower;
using Superpower.Parsers;

namespace JavaPropertiesParser.Tokenization
{
    public static class Values
    {
        private static readonly TextParser<TokenType> ValueCharParser = Span
            .WithAll(c => c != '\\' && c != '\r' && c != '\n')
            .Value(TokenType.ValueChars);

        private static readonly TextParser<TokenType> LogicalNewLineParser = Character
            .In('r', 'n')
            .Value(TokenType.ValueEscapeSequence);
        
        private static readonly TextParser<TokenType> PhysicalNewLineParser =
            from newline in Common.NewLineParser
            from indentation in Common.WhitespaceCharacterParser.Many()
            select TokenType.ValuePhysicalNewLine;

        private static readonly TextParser<TokenType> UnicodeEscapeSequenceParser =             
            from start in Character.EqualTo('u')
            from digits in Character.HexDigit.Repeat(4)
            select TokenType.ValueUnicodeEscapeSequence;

        private static readonly TextParser<TokenType> EscapedParser =
            from slash in Character.EqualTo('\\')
            from parsed in LogicalNewLineParser
                .Or(PhysicalNewLineParser)
                .Or(UnicodeEscapeSequenceParser)
            select parsed;

        public static readonly TextParser<TokenType> Parser = ValueCharParser
            .Or(EscapedParser);
    }
}