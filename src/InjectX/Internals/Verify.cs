namespace InjectX.Internals;

[DebuggerStepThrough]
internal static partial class Verify
{
    internal static void NotUnmanagedCall()
    {
        if (Assembly.GetEntryAssembly() is null)
            ThrowInvalidOperation(SR.UnmanagedCall);
    }

    internal static void NotNull<T>([NotNull] T? argument, [CallerArgumentExpression(nameof(argument))] string paramName = "")
    {
        if (argument is null)
            ThrowArgumentNull(paramName);
    }

    [DoesNotReturn]
    private static void ThrowArgumentNull(string? paramName)
        => throw new ArgumentNullException(paramName);

    [DoesNotReturn]
    private static void ThrowInvalidOperation(string? message) 
        => throw new InvalidOperationException(message);
}
