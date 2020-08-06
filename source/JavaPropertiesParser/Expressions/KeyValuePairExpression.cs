namespace JavaPropertiesParser.Expressions
{
    public class KeyValuePairExpression : ITopLevelExpression
    {
        public KeyValuePairExpression(KeyExpression keyExpression, SeparatorExpression separatorExpression, ValueExpression valueExpression)
        {
            KeyExpression = keyExpression;
            SeparatorExpression = separatorExpression;
            ValueExpression = valueExpression;
        }
        
        public KeyExpression KeyExpression { get; }
        
        public SeparatorExpression SeparatorExpression { get; }
        
        public ValueExpression ValueExpression { get; }

        public override string ToString()
        {
            return KeyExpression.ToString() + SeparatorExpression + ValueExpression;
        }

        protected bool Equals(KeyValuePairExpression other)
        {
            return Equals(KeyExpression, other.KeyExpression) 
                && Equals(SeparatorExpression, other.SeparatorExpression) 
                && Equals(ValueExpression, other.ValueExpression);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KeyValuePairExpression) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = KeyExpression != null ? KeyExpression.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (SeparatorExpression != null ? SeparatorExpression.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ValueExpression != null ? ValueExpression.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}