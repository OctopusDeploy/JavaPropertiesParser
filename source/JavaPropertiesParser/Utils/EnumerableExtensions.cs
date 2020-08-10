using System.Collections.Generic; 
using System.Linq; 
 
namespace JavaPropertiesParser.Utils 
{ 
    public static class EnumerableExtensions 
    {
        public static int AggregateHashCode<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                return 0;
            }

            // Based on Rider's auto generated GetHashCode() methods. 
            return sequence.Aggregate(0, (prev, next) => (prev * 397) ^ next?.GetHashCode() ?? 0);
        }

        public static StringValue Join(this IEnumerable<StringValue> values)
        {
            return values.Aggregate(
                new StringValue("", ""),
                (a, b) => a + b
            );
        }
    } 
}