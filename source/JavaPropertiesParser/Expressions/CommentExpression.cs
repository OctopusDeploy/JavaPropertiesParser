namespace JavaPropertiesParser.Expressions
{
    public class CommentExpression : ITopLevelExpression
    {
        public CommentExpression(char delimiter, string text)
        {
            Delimiter = delimiter;
            Text = text;
        }
        
        public char Delimiter { get; }
        
        public string Text { get; }

        protected bool Equals(CommentExpression other)
        {
            return Delimiter == other.Delimiter && Text == other.Text;
        }

        public override string ToString()
        {
            return Delimiter + Text;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CommentExpression) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Delimiter.GetHashCode() * 397) ^ (Text != null ? Text.GetHashCode() : 0);
            }
        }
    }
}