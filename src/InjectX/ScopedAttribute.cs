namespace InjectX;

/// <summary>
/// Specifies that a service should be registered with a <see cref="ServiceLifetime"/> of <see cref="ServiceLifetime.Scoped"/>. 
/// This class cannot be inherited.
/// </summary>
public sealed class ScopedAttribute : DescriptorAttribute
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Scoped;
}
