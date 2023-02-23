namespace InjectX.Internals;

internal static class ResourceExtensions
{
    internal static string Format(this string str, params object[] args)
    {
        return string.Format(str, args);
    }
}
