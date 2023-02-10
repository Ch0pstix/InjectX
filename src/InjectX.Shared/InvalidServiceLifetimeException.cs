namespace InjectX.Shared;

/// <summary>
/// The exception that is thrown when registering a service with an invalid <see cref="ServiceLifetime"/>.
/// </summary>
public class InvalidServiceLifetimeException : InvalidOperationException
{
    /// <summary>
    /// Initializes an instance of the <see cref="InvalidServiceLifetimeException"/> class.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="lifetime">The service lifetime.</param>
    public InvalidServiceLifetimeException(Type service, ServiceLifetime lifetime)
        : base($"The service lifetime {lifetime} is not valid for the service '{service}'")
    {
        Service = service;
        Lifetime = lifetime;
    }

    /// <summary>
    /// The service.
    /// </summary>
    public Type Service { get; }

    /// <summary>
    /// The service lifetime.
    /// </summary>
    public ServiceLifetime Lifetime { get; }
}
