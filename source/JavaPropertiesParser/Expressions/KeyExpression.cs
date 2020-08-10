using JavaPropertiesParser.Utils;

namespace JavaPropertiesParser.Expressions
{
    public class KeyExpression : IExpression
    {
        public KeyExpression(StringValue text)
        {
            Text = text;
        }
        
        public StringValue Text { get; }

        public override string ToString()
        {
            return Text.EncodedValue;
        }

        protected bool Equals(KeyExpression other)
        {
            return Text.Equals(other.Text);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KeyExpression) obj);
        }

        public override int GetHashCode()
        {
            return Text != null ? Text.GetHashCode() : 0;
        }
    }
}