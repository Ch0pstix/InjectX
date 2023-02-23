namespace InjectX.Internals;

[DebuggerStepThrough]
internal static class EnumerableExtensions
{
    internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        where T : notnull
    {
        foreach (T item in source) action(item);
    }

    internal static void Fork<T>(this IEnumerable<T> source, Func<T, bool> predicate, out IEnumerable<T> matches, out IEnumerable<T> nonMatches)
        where T : notnull
    {
        ILookup<bool, T> filteredItems = source.ToLookup(predicate);

        matches = filteredItems[true];
        nonMatches = filteredItems[false];
    }
}
