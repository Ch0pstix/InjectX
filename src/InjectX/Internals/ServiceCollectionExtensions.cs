namespace InjectX.Internals;

[DebuggerStepThrough]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection TryAddWithLifetime(this IServiceCollection services, Type serviceType, ServiceLifetime lifetime)
    {
        Verify.NotNull(services);
        Verify.NotNull(serviceType);
        Verify.NotNull(lifetime);

        ServiceDescriptor descriptor = ServiceDescriptor.Describe(serviceType, serviceType, lifetime);
        services.TryAdd(descriptor);

        return services;
    }
}
