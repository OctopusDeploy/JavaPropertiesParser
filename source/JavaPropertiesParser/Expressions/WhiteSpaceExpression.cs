using Superpower.Model;

namespace JavaPropertiesParser.Expressions
{
    public class WhiteSpaceExpression : ITopLevelExpression
    {
        public WhiteSpaceExpression(Token<TokenType> token) : this(token.ToStringValue())
        {
        }

        public WhiteSpaceExpression(string content)
        {
            Content = content;
        }

        public string Content { get; }

        public override string ToString()
        {
            return Content;
        }

        protected bool Equals(WhiteSpaceExpression other)
        {
            return Content == other.Content;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WhiteSpaceExpression) obj);
        }

        public override int GetHashCode()
        {
            return Content != null ? Content.GetHashCode() : 0;
        }
    }
}