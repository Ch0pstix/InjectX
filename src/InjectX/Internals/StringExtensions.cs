namespace InjectX.Internals;

[DebuggerStepThrough]
internal static partial class StringExtensions
{
    internal static string Format(this string str, params object[] args)
    {
        return string.Format(str, args);
    }

    internal static bool XOR(this string str, char first, char second)
    {
        bool hasFirst = str.Contains(first), hasSecond = str.Contains(second);
        return (hasFirst && hasSecond) || (!hasFirst && !hasSecond);
    }
}
