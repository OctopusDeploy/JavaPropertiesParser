using JavaPropertiesParser.Expressions;

namespace JavaPropertiesParser
{
    public static class Build
    {
        public static Document Doc(params ITopLevelExpression[] expressions)
        {
            return new Document(expressions);
        }
        
        public static CommentExpression HashComment(string text)
        {
            return new CommentExpression('#', text);
        }
        
        public static CommentExpression BangComment(string text)
        {
            return new CommentExpression('!', text);
        }
        
        public static WhitespaceExpression Whitespace(string text)
        {
            return new WhitespaceExpression(text);
        }

        public static KeyExpression Key(string text)
        {
            return new KeyExpression(text);
        }

        public static ValueExpression Value(string text)
        {
            return new ValueExpression(text);
        }
        
        public static SeparatorExpression Separator(string text)
        {
            return new SeparatorExpression(text);
        }

        public static KeyValuePairExpression Pair(
            KeyExpression key, 
            SeparatorExpression separator,
            ValueExpression value)
        {
            return new KeyValuePairExpression(key, separator, value);
        }
    }
}