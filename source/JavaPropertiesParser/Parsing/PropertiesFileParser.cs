using JavaPropertiesParser.Expressions;
using JavaPropertiesParser.Utils;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace JavaPropertiesParser.Parsing
{
    public static class PropertiesFileParser
    {
        private static readonly TokenListParser<TokenType, Token<TokenType>> KeyComponent = Tokens.In(
            TokenType.KeyChars,
            TokenType.KeyEscapeSequence,
            TokenType.KeyPhysicalNewLine,
            TokenType.KeyUnicodeEscapeSequence
        );
        
        private static readonly TokenListParser<TokenType, Token<TokenType>> ValueComponent = Tokens.In(
            TokenType.ValueChars,
            TokenType.ValueEscapeSequence,
            TokenType.ValuePhysicalNewLine,
            TokenType.ValueUnicodeEscapeSequence
        );
        
        private static readonly TokenListParser<TokenType, KeyExpression> KeyParser = 
            from tokens in KeyComponent.AtLeastOnce()
            select new KeyExpression(tokens);
        
        private static readonly TokenListParser<TokenType, SeparatorExpression> SeparatorParser = 
            from token in Token.EqualTo(TokenType.Separator)
            select new SeparatorExpression(token);
        
        private static readonly TokenListParser<TokenType, ValueExpression> ValueParser = 
            from tokens in ValueComponent.AtLeastOnce()
            select new ValueExpression(tokens);
        
        private static readonly TokenListParser<TokenType, ITopLevelExpression> KeyValuePairParser =
            from key in KeyParser
            from separator in SeparatorParser.OptionalOrDefault()
            from value in ValueParser.OptionalOrDefault()
            select (ITopLevelExpression)new KeyValuePairExpression(key, separator, value);

        private static readonly TokenListParser<TokenType, ITopLevelExpression> CommentParser = 
            from token in Token.EqualTo(TokenType.Comment)
            select (ITopLevelExpression)new CommentExpression(token);
        
        private static readonly TokenListParser<TokenType, ITopLevelExpression> WhiteSpaceParser = 
            from token in Token.EqualTo(TokenType.Whitespace)
            select (ITopLevelExpression)new WhiteSpaceExpression(token);

        private static readonly TokenListParser<TokenType, ITopLevelExpression> TopLevelExpressions = KeyValuePairParser
            .Or(CommentParser)
            .Or(WhiteSpaceParser);

        private static readonly TokenListParser<TokenType, PropertiesDocument> DocumentParser = 
            from expressions in TopLevelExpressions.Many()
            select new PropertiesDocument(expressions);

        public static TokenListParserResult<TokenType, PropertiesDocument> Parse(TokenList<TokenType> tokens)
        {
            return DocumentParser(tokens);
        }
    }
}