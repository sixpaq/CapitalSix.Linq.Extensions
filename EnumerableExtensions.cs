namespace CapitalSix.Linq.Extensions;

/// <summary>
/// This extension contains a number of methods
/// that handle IEnumerable collections.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Iterates an enumerable and aborts when the cancelation token
    /// is triggered
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The enumerable</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    public static IEnumerable<T> AsCancellable<T>(this IEnumerable<T> source, CancellationToken cancellationToken)
    {
        var iterator = source.GetEnumerator();
        while (iterator.MoveNext())
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return iterator.Current;
        }
    }

    /// <summary>
    /// Waits for all tasks, but yields tasks that are ready until
    /// all are finished.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tasks">The list of tasks</param>
    /// <returns>An async enumerable containing finished tasks</returns>
    public static async IAsyncEnumerable<Task<T>> WhenEachAsync<T>(this IEnumerable<Task<T>> tasks)
    {
        var taskList = tasks.ToList();
        while (taskList.Any())
        {
            var task = await Task.WhenAny(taskList);
            taskList.Remove(task);
            yield return task;
        }
    }

    /// <summary>
    /// SelectMany but in async form
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static async IAsyncEnumerable<TResult> SelectManyAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TResult>> func)
    {
        var iterator = source.GetAsyncEnumerator();
        while (await iterator.MoveNextAsync())
        {
            var subIterator = (iterator.Current as IAsyncEnumerable<TResult>)!.GetAsyncEnumerator();
            while (await subIterator.MoveNextAsync())
            {
                yield return subIterator.Current;
            }
        }
    }
}