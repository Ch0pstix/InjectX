namespace InjectX;

/// <summary>
/// Defines extension methods for the automatic registration of service objects to an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers service objects that have been defined within the assembly of the current application.
    /// </summary>
    /// <remarks>
    /// Below is the default lifetime for service objects:
    /// <list type="bullet">
    /// <item><see cref="ServiceLifetime.Transient"/></item>
    /// </list>
    /// The lifetime for a service object may be overridden by annotating it with one of the following attributes:
    /// <list type="bullet">
    /// <item><see cref="SingletonAttribute"/></item>
    /// <item><see cref="TransientAttribute"/></item>
    /// <item><see cref="ScopedAttribute"/></item>
    /// </list>
    /// </remarks>
    /// <param name="services">The service collection.</param>
    /// <param name="strategy">The strategy to use when adding new services to the service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls may be chained.</returns>
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, RegistrationStrategy? strategy = null)
    {
        Verify.NotNull(services);
        Verify.ApplicationContext();

        return services.RegisterAssemblyServices(Assembly.GetEntryAssembly(), strategy);
    }

    /// <summary>
    /// Registers service objects that have been defined within <paramref name="assembly"/>.
    /// </summary>
    /// <remarks>
    /// Below is the default lifetime for service objects:
    /// <list type="bullet">
    /// <item><see cref="ServiceLifetime.Transient"/></item>
    /// </list>
    /// The lifetime for a service object may be overridden by annotating it with one of the following attributes:
    /// <list type="bullet">
    /// <item><see cref="SingletonAttribute"/></item>
    /// <item><see cref="TransientAttribute"/></item>
    /// <item><see cref="ScopedAttribute"/></item>
    /// </list>
    /// </remarks>
    /// <param name="services">The service collection.</param>
    /// <param name="assembly">The assembly containing the service objects.</param>
    /// <param name="strategy">The strategy to use when adding new services to the service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls may be chained.</returns>
    public static IServiceCollection RegisterAssemblyServices(this IServiceCollection services, Assembly assembly, RegistrationStrategy? strategy = null)
    {
        Verify.NotNull(services);
        Verify.NotNull(assembly);

        string rootNamespace = assembly.GetRootNamespace();
        strategy ??= RegistrationStrategy.Append;

        assembly
            .GetTypes()
            .Where(type => 
                type.IsPublic && 
                type.Namespace is not null &&
                type.Namespace.Equals($"{rootNamespace}.Services", StringComparison.Ordinal))
            .Fork(type => 
                type.IsInterface, 
                out IEnumerable<Type> serviceTypes, 
                out IEnumerable<Type> implementationTypes);

        implementationTypes
            .ForEach(implementationType =>
            {
                Type serviceType = implementationType
                    .GetInterfaces()
                    .Where(contract => serviceTypes.Contains(contract))
                    .FirstOrDefault()
                    ?? implementationType;

                ServiceLifetime lifetime = implementationType.GetCustomAttribute<ServiceDescriptorAttribute>()?.Lifetime
                    ?? ServiceLifetime.Transient;

                ServiceDescriptor descriptor = ServiceDescriptor.Describe(serviceType, implementationType, lifetime);

                strategy.Execute(services, descriptor);
            });

        return services;
    }
}
