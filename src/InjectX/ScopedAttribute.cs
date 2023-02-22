namespace InjectX;

/// <summary>
/// Specifies that the object associated with this attribute should be registered with a lifetime of <see cref="ServiceLifetime.Scoped"/>.<br/>
/// This class cannot be inherited.
/// </summary>
public sealed class ScopedAttribute : ServiceDescriptorAttribute
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Scoped;
}

/// <summary>
/// Specifies that the object associated with this attribute should be registered to the DI container with a lifetime of <see cref="ServiceLifetime.Scoped"/>.<br/>
/// This class cannot be inherited.
/// </summary>
/// <typeparam name="TService">The service type.</typeparam>
public sealed class ScopedAttribute<TService> : ServiceDescriptorAttribute
    where TService : class
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Scoped;
}
