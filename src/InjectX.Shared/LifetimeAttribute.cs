namespace InjectX.Shared;

/// <summary>
/// Base class for the <see cref="ServiceLifetime"/> attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public abstract class LifetimeAttribute : Attribute
{
    /// <summary>
    /// Specifies the <see cref="ServiceLifetime"/> for a decorated class.
    /// </summary>
    public abstract ServiceLifetime Lifetime { get; }
}
