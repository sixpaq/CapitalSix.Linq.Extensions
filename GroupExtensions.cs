namespace CapitalSix.Linq.Extensions;

/// <summary>
/// This extension groups elements based on a key definition and on a equality comparer.
/// This is similar to Linq's GroupBy, but this returns a dictionary with all matched values.
/// </summary>
public static class GroupExtensions
{
    /// <summary>
    /// Create a dictionary with grouped items, based on a custom comparer function
    /// </summary>
    /// <param name="source">The source list</param>
    /// <param name="keySelector">The key selector</param>
    /// <param name="elementSelector">The element selector. This expression defines the elements in the dictionary's values</param>
    /// <param name="comparer">A equality comparer based on which items should be grouped</param>
    /// <returns>A dictionary with grouped items</returns>
    public static IDictionary<TKey, IEnumerable<TElement>> Group<TSource, TKey, TElement>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TElement> elementSelector,
        IEqualityComparer<TKey>? comparer)
        where TKey : notnull
        => source
            .GroupBy(keySelector, elementSelector, comparer)
            .ToDictionary(x => x.Key, y => y.ToList().AsEnumerable());

    /// <summary>
    /// Create a dictionary with grouped items, based on a custom comparer function
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source">The source list</param>
    /// <param name="comparer">A equality comparer based on which items should be grouped</param>
    /// <returns>A dictionary with grouped items</returns>
    public static IDictionary<TSource, IEnumerable<TSource>> Group<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource>? comparer)
        where TSource : notnull
        => source.Group(x => x, x => x, comparer);

    /// <summary>
    /// Create a dictionary with grouped items, based on a custom comparer function
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="source">The source list</param>
    /// <param name="elementSelector">The element selector. This expression defines the elements in the dictionary's values</param>
    /// <param name="comparer">A equality comparer based on which items should be grouped</param>
    /// <returns>A dictionary with grouped items</returns>
    public static IDictionary<TSource, IEnumerable<TElement>> Group<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, TElement> elementSelector, IEqualityComparer<TSource>? comparer)
        where TSource : notnull
        => source.Group(x => x, elementSelector, comparer);

    /// <summary>
    /// Create a dictionary with grouped items, based on a custom comparer function
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source">The source list</param>
    /// <param name="keySelector">The key selector</param>
    /// <returns>A dictionary with grouped items</returns>
    public static IDictionary<TKey, IEnumerable<TSource>> Group<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        where TKey : notnull
        => source.Group(keySelector, x => x, null);

    /// <summary>
    /// Create a dictionary with grouped items, based on a custom comparer function
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="source">The source list</param>
    /// <param name="keySelector">The key selector</param>
    /// <param name="elementSelector">The element selector. This expression defines the elements in the dictionary's values</param>
    /// <returns>A dictionary with grouped items</returns>
    public static IDictionary<TKey, IEnumerable<TElement>> Group<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        where TKey : notnull
        => source.Group(keySelector, elementSelector, null);

    /// <summary>
    /// Create a dictionary with grouped items, based on a custom comparer function
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source">The source list</param>
    /// <returns>A dictionary with grouped items</returns>
    public static IDictionary<TSource, IEnumerable<TSource>> Group<TSource>(this IEnumerable<TSource> source)
        where TSource : notnull
        => source.Group(x => x, x => x, null);
}

