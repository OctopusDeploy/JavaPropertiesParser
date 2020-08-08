using System;

namespace JavaPropertiesParser.Utils
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Applies a mutator to an array of items. If any item is mutated,
        /// a new array is returned with the mutated items. If no item is
        /// mutated, the source array is returned. Either way the source
        /// array is left unchanged. Mutated items are compared to their
        /// source item by reference equality.
        /// </summary>
        public static T[] Mutate<T>(this T[] source, Func<T, T> mutator)
        {
            var anyItemHasChanged = false;
            var newArray = new T[source.Length];
            
            for (var i = 0; i < source.Length; i++)
            {
                var item = source[i];
                var mutated = mutator(item);
                newArray[i] = mutated;
                
                if (!ReferenceEquals(item, mutated))
                {
                    anyItemHasChanged = true;
                }
            }

            return anyItemHasChanged ? newArray : source;
        }
    }
}