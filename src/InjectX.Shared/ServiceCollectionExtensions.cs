namespace InjectX.Shared;

/// <summary>
/// Defines extension methods for <see cref="IServiceCollection"/>.
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="ServiceDescriptor"/> with a <see cref="ServiceDescriptor.ServiceType"/> and <see cref="ServiceDescriptor.ImplementationType"/> of <paramref name="serviceType"/> to <paramref name="services"/> if the service type hasn't already been registered.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="serviceType">The service type.</param>
    /// <param name="lifetime">The service lifetime.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls may be chained.</returns>
    internal static IServiceCollection TryAddWithLifetime(this IServiceCollection services, Type serviceType, ServiceLifetime lifetime)
    {
        Verify.NotNull(services);
        Verify.NotNull(serviceType);
        Verify.NotNull(lifetime);

        services.TryAdd(ServiceDescriptor.Describe(serviceType, serviceType, lifetime));

        return services;
    }
}
