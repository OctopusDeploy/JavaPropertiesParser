namespace JavaPropertiesParser.Expressions
{
    public class KeyValuePairExpression : ITopLevelExpression
    {
        public KeyValuePairExpression(KeyExpression key, SeparatorExpression separator, ValueExpression value)
        {
            Key = key;
            Separator = separator;
            Value = value;
        }
        
        public KeyExpression Key { get; }
        
        public SeparatorExpression Separator { get; }
        
        public ValueExpression Value { get; }

        public override string ToString()
        {
            return Key?.ToString() + Separator + Value;
        }

        protected bool Equals(KeyValuePairExpression other)
        {
            return Equals(Key, other.Key) && Equals(Separator, other.Separator) && Equals(Value, other.Value);
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
                var hashCode = Key != null ? Key.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Separator != null ? Separator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}