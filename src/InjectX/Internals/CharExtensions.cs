namespace InjectX.Internals;

[DebuggerStepThrough]
internal static class CharExtensions
{
    internal static bool IsAsciiAlphanumeric(this char c)
    {
        return (c >= 'a' && c <= 'z') ||
               (c >= 'A' && c <= 'Z') ||
               (c >= '0' && c <= '9');
    }
}
