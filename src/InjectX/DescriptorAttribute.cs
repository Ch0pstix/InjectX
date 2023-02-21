namespace InjectX;

/// <summary>
/// Base class for the <see cref="ServiceLifetime"/> attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public abstract class DescriptorAttribute : Attribute
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
