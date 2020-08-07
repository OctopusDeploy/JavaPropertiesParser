namespace JavaPropertiesParser.Expressions
{
    public class WhitespaceExpression : ITopLevelExpression
    {
        public WhitespaceExpression(string text)
        {
            Text = text;
        }
        
        public string Text { get; }

        public override string ToString()
        {
            return Text;
        }

        protected bool Equals(WhitespaceExpression other)
        {
            return Text == other.Text;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WhitespaceExpression) obj);
        }

        public override int GetHashCode()
        {
            return Text != null ? Text.GetHashCode() : 0;
        }
    }
}