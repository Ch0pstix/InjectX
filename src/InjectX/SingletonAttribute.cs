namespace InjectX;

/// <summary>
/// Specifies that a view or service should be registered with a <see cref="ServiceLifetime"/> of <see cref="ServiceLifetime.Singleton"/>. 
/// This class cannot be inherited.
/// </summary>
public sealed class SingletonAttribute : DescriptorAttribute
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Singleton;
}
