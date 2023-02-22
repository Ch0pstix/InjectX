namespace InjectX;

/// <summary>
/// Specifies that the object associated with this attribute should be registered with a lifetime of <see cref="ServiceLifetime.Transient"/>.<br/>
/// This class cannot be inherited.
/// </summary>
public sealed class TransientAttribute : ServiceDescriptorAttribute
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Transient;
}

/// <summary>
/// Specifies that the object associated with this attribute should be registered to the DI container with a lifetime of <see cref="ServiceLifetime.Transient"/>.<br/>
/// This class cannot be inherited.
/// </summary>
/// <typeparam name="TService">The service type.</typeparam>
public sealed class TransientAttribute<TService> : ServiceDescriptorAttribute
    where TService : class
{
    /// <inheritdoc/>
    public override ServiceLifetime Lifetime => ServiceLifetime.Transient;
}
