using System.Collections.Generic;
using System.IO;

namespace CapitalSix.Linq.Extensions;

/// <summary>
/// The chunk extension splits a collection in preferred size parts and returns the "Chuncks" in a list.
/// </summary>
public static class ChunkExtensions
{
    /// <summary>
    /// The default chunk size
    /// </summary>
    public const int DEFAULT_CHUNK_SIZE = 2000;

    /// <summary>
    /// Process a list of T in chunks of size {chunkSize}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="all">The original list of items</param>
    /// <param name="chunkAction">Action to perform on each chunk</param>
    /// <param name="chunkSize">Size of each chunk</param>
    /// <returns>The original list of items</returns>
    public static IEnumerable<T> Chunked<T>(this IEnumerable<T> all, Action<IEnumerable<T>> chunkAction, int chunkSize = DEFAULT_CHUNK_SIZE)
    {
        IEnumerable<IEnumerable<T>> chunks = all.Chunks(chunkSize);

        foreach (IEnumerable<T> chunk in chunks)
        {
            chunkAction(chunk);
        }
        return all;
    }

    /// <summary>
    /// Split an enumerable set into chunks of size {chunkSize}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="all">The original list of items</param>
    /// <param name="chunkSize">Size of each chunk</param>
    /// <returns>A list of chunks</returns>
    public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> all, int chunkSize = DEFAULT_CHUNK_SIZE)
        => all
            .Select((item, index) => (Item: item, Index: index))
            .GroupBy(chunkItem => chunkItem.Index / chunkSize)
            .Select(chunkItem => chunkItem.Select(x => x.Item).ToList());
}
