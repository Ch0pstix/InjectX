namespace InjectX.Mvvm;

/// <summary>
/// Defines extension methods for the automatic registration of view and viewmodel objects to an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers view and viewmodel objects that have been defined within the assembly of the current application.
    /// </summary>
    /// <remarks>
    /// Below are the default lifetimes for views and viewmodel objects:
    /// <list type="bullet">
    /// <item>View <see cref="ServiceLifetime.Transient"/></item>
    /// <item>Viewmodel - <see cref="ServiceLifetime.Transient"/></item>
    /// </list>
    /// The lifetime for a view object may be overridden by annotating it with one of the following attributes:
    /// <list type="bullet">
    /// <item><see cref="SingletonAttribute"/></item>
    /// <item><see cref="TransientAttribute"/></item>
    /// </list>
    /// </remarks>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls may be chained.</returns>
    public static IServiceCollection RegisterViewsAndViewModels(this IServiceCollection services)
    {
        Verify.NotNull(services);
        Verify.NotUnmanagedCall();

        Assembly assembly = Assembly.GetEntryAssembly()!;
        string rootNamespace = assembly.GetRootNamespace();

        IEnumerable<Type> types = assembly
            .GetTypes()
            .Where(type => 
                type.IsPublic &&
                type.IsClass &&
                type.Namespace is not null);

        types
            .Where(type => type.Namespace!.StartsWith($"{rootNamespace}.ViewModels", StringComparison.Ordinal))
            .ForEach(viewmodel => services.TryAddWithLifetime(viewmodel, ServiceLifetime.Transient));

        types
            .Where(type => type.Namespace!.StartsWith($"{rootNamespace}.Views", StringComparison.Ordinal))
            .ForEach(view =>
            {
                ServiceLifetime lifetime = view.GetCustomAttribute<ServiceDescriptorAttribute>()?.Lifetime
                    ?? ServiceLifetime.Transient;

                services.TryAddWithLifetime(view, lifetime);
            });

        return services;
    }
}
