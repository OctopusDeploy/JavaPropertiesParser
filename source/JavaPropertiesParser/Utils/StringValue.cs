namespace JavaPropertiesParser.Utils
{
    public class StringValue
    {
        public StringValue(string logicalValue, string encodedValue)
        {
            LogicalValue = logicalValue;
            EncodedValue = encodedValue;
        }
        
        public string LogicalValue { get; }
        
        public string EncodedValue { get; }

        protected bool Equals(StringValue other)
        {
            return LogicalValue == other.LogicalValue && EncodedValue == other.EncodedValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StringValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((LogicalValue != null ? LogicalValue.GetHashCode() : 0) * 397) ^ (EncodedValue != null ? EncodedValue.GetHashCode() : 0);
            }
        }

        public static StringValue operator +(StringValue a, StringValue b)
        {
            return new StringValue(a.LogicalValue + b.LogicalValue, a.EncodedValue + b.EncodedValue);
        }
    }
}