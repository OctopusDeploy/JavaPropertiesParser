using JavaPropertiesParser.Expressions;
using Superpower.Model;

namespace JavaPropertiesParser
{
    public static class Build
    {
        public static PropertiesDocument Doc(params ITopLevelExpression[] expressions)
        {
            return new PropertiesDocument(expressions);
        }

        public static CommentExpression HashComment(string content)
        {
            return new CommentExpression("#" + content);
        }

        public static CommentExpression ExclamationComment(string content)
        {
            return new CommentExpression("!" + content);
        }
        
        public static CommentExpression BangComment(string content) => ExclamationComment(content);

        public static WhiteSpaceExpression Whitespace(string content)
        {
            return new WhiteSpaceExpression(content);
        }
        
        public static KeyValuePairExpression Pair(KeyExpression keyExpression, SeparatorExpression separatorExpression = null, ValueExpression valueExpression = null)
        {
            return new KeyValuePairExpression(keyExpression, separatorExpression, valueExpression);
        }

        public static KeyExpression Key(string serializableValue)
        {
            return new KeyExpression(KeyChars(serializableValue));
        }

        public static SeparatorExpression Separator(string serializableValue)
        {
            return new SeparatorExpression(Token(TokenType.Separator, serializableValue));
        }

        public static ValueExpression Value(string serializableValue)
        {
            return new ValueExpression(Token(TokenType.ValueChars, serializableValue));
        }

        public static ValueExpression Value(params Token<TokenType>[] components)
        {
            return new ValueExpression(components);
        }
        
        private static Token<TokenType> Token(TokenType type, string serializableValue)
        {
            return new Token<TokenType>(type, new TextSpan(serializableValue));
        }

        public static Token<TokenType> KeyChars(string serializableValue)
        {
            return Token(TokenType.KeyChars, serializableValue);
        }

        public static Token<TokenType> ValueChars(string serializableValue)
        {
            return Token(TokenType.ValueChars, serializableValue);
        }

    }
}