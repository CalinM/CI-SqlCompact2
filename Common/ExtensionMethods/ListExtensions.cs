using System;
using System.Collections.Generic;

namespace Common.ExtensionMethods
{
    public static class ListExtensions
    {
        //var query = people.DistinctBy(p => p.Id);
        //var query = people.DistinctBy(p => new { p.Id, p.Name });
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
