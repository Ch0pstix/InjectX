namespace InjectX;

/// <summary>
/// Base class for <see cref="ServiceDescriptor"/> attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public abstract class ServiceDescriptorAttribute : Attribute
{
    /// <summary>
    /// Specifies the <see cref="ServiceLifetime"/> for the service.
    /// </summary>
    public abstract ServiceLifetime Lifetime { get; }

    /// <summary>
    /// Specifies the <see cref="Type"/> of the service.
    /// </summary>
    public Type? ServiceType { get; set; }
}

/// <summary>
/// Base class for generic <see cref="ServiceDescriptor"/> attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public abstract class ServiceDescriptorAttribute<TService> : Attribute
    where TService : class
{
    /// <summary>
    /// Specifies the <see cref="ServiceLifetime"/> for the service.
    /// </summary>
    public abstract ServiceLifetime Lifetime { get; }

    /// <summary>
    /// Specifies the <see cref="Type"/> of the service.
    /// </summary>
    public Type ServiceType => typeof(TService);
}
