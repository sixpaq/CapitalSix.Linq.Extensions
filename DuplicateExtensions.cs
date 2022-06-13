namespace CapitalSix.Linq.Extensions;

public static class DuplicateExtensions
{
    /// <summary>
    /// Create a dictionary of all duplicate items based on a key and a comparer
    /// </summary>
    /// <param name="source">The source list</param>
    /// <param name="selector">A key selector</param>
    /// <param name="comparer">A equality comparer</param>
    /// <returns>A dictionary with all duplicate items grouped together</returns>
    public static IDictionary<TKey, IEnumerable<TSource>> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IEqualityComparer<TKey>? comparer)
        where TKey : notnull
        => source
            .Group(selector, x => x, comparer)
            .Where(g => g.Value.Count() > 1)
            .ToDictionary(x => x.Key, y => y.Value);

    public static IDictionary<TSource, IEnumerable<TSource>> Duplicates<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource>? comparer)
        where TSource : notnull
        => source.Duplicates(x => x, comparer);

    public static IDictionary<TKey, IEnumerable<TSource>> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        where TKey : notnull
        => source.Duplicates(selector, null);

    public static IDictionary<TSource, IEnumerable<TSource>> Duplicates<TSource>(this IEnumerable<TSource> source)
        where TSource : notnull
        => source.Duplicates(x => x, null);
}
