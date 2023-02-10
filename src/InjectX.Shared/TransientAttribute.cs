namespace InjectX.Shared;

/// <summary>
/// Specifies that a view or service should be registered with a <see cref="ServiceLifetime"/> of <see cref="ServiceLifetime.Transient"/>. 
/// This class cannot be inherited.
/// </summary>
public sealed class TransientAttribute : LifetimeAttribute
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Transient;
}
