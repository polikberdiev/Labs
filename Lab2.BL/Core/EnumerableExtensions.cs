using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.BL.Core
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this ICollection<T> source, int size)
        {
            for (var i = 0; i < source.Count; i += size)
            {
                yield return source.Skip(i).Take(size);
            }
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            bool asc)
        {
            return asc
                ? source.OrderBy(keySelector)
                : source.OrderByDescending(keySelector);
        }
    }
}