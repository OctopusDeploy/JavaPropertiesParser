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
            T[] newArray = null;
            
            for (var i = 0; i < source.Length; i++)
            {
                var item = source[i];
                var mutated = mutator(item);
                if (!anyItemHasChanged && !ReferenceEquals(item, mutated))
                {
                    newArray = new T[source.Length];
                    Array.Copy(source, 0, newArray, 0, i);
                    
                    newArray[i] = mutated;
                    anyItemHasChanged = true;
                }
                else if (anyItemHasChanged)
                {
                    newArray[i] = mutated;
                }
            }

            return anyItemHasChanged ? newArray : source;
        }
    }
}