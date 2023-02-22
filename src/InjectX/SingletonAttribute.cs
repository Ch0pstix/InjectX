namespace InjectX;

/// <summary>
/// Specifies that the object associated with this attribute should be registered with a lifetime of <see cref="ServiceLifetime.Singleton"/>.<br/>
/// This class cannot be inherited.
/// </summary>
public sealed class SingletonAttribute : ServiceDescriptorAttribute
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Singleton;
}

/// <summary>
/// Specifies that the object associated with this attribute should be registered to the DI container with a lifetime of <see cref="ServiceLifetime.Singleton"/>.<br/>
/// This class cannot be inherited.
/// </summary>
/// <typeparam name="TService">The service type.</typeparam>
public sealed class SingletonAttribute<TService> : ServiceDescriptorAttribute
    where TService : class
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Singleton;
}
