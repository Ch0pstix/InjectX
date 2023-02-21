namespace InjectX;

/// <summary>
/// Provides extension methods for the <see cref="IEnumerable{T}"/> class.
/// </summary>
internal static class EnumerableExtensions
{
    /// <summary>
    /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to perform.</param>
    internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        where T: notnull
    {
        Verify.NotNull(source);
        Verify.NotNull(action);

        foreach (T item in source) action(item);
    }

    /// <summary>
    /// Filters the <see cref="IEnumerable{T}"/> into two new sequences by evaluating each element against the predicate.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="predicate">The predicate to match the elements against.</param>
    /// <param name="matches">The <see cref="IEnumerable{T}"/> of elements that matched the predicate.</param>
    /// <param name="nonMatches">The <see cref="IEnumerable{T}"/> of elements that did not match the predicate.</param>
    internal static void Fork<T>(this IEnumerable<T> source, Func<T, bool> predicate, out IEnumerable<T> matches, out IEnumerable<T> nonMatches)
        where T: notnull
    {
        Verify.NotNull(source);
        Verify.NotNull(predicate);

        ILookup<bool, T> filteredItems = source.ToLookup(predicate);

        matches = filteredItems[true];
        nonMatches = filteredItems[false];
    }
}
