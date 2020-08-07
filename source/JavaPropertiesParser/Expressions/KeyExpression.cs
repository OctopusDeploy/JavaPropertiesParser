namespace JavaPropertiesParser.Expressions
{
    public class KeyExpression : IExpression
    {
        public KeyExpression(string text)
        {
            Text = text;
        }
        
        public string Text { get; }

        public override string ToString()
        {
            return Text;
        }

        protected bool Equals(KeyExpression other)
        {
            return Text == other.Text;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyExpression) obj);
        }

        public override int GetHashCode()
        {
            return (Text != null ? Text.GetHashCode() : 0);
        }
    }
}