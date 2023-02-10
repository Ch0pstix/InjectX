namespace InjectX.Shared;

/// <summary>
/// Used to verify the state of various objects.
/// </summary>
internal static partial class Verify
{
    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if <see cref="Assembly.GetEntryAssembly()"/> returns <see langword="null"/>.
    /// </summary>h
    /// <exception cref="InvalidOperationException"></exception>
    internal static void AppDomainExists()
    {
        _ = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Application domain not found.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the specified argument is <see langword="null"/>.
    /// </summary>
    /// <param name="arg">The argument to check.</param>
    /// <param name="argName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal static void NotNull(object arg, [CallerArgumentExpression(nameof(arg))] string argName = "")
    {
        _ = arg ?? throw new ArgumentNullException(argName);
    }

    /// <summary>
    /// Throws a <see cref="DuplicateServiceException"/> if <paramref name="descriptor"/> already exists in <paramref name="services"/>.
    /// </summary>
    /// <param name="descriptor">The descriptor to check for.</param>
    /// <param name="services">The service collection to check.</param>
    /// <exception cref="DuplicateServiceException"></exception>
    internal static void UniqueDescriptor(ServiceDescriptor descriptor, IServiceCollection services)
    {
        if (services.Any(d => d.ServiceType == descriptor.ServiceType))
            throw new DuplicateServiceException(services, descriptor);
    }

    /// <summary>
    /// Throws an <see cref="InvalidServiceLifetimeException"/> if the <see cref="ServiceLifetime"/> for the <paramref name="view"/> is <see cref="ServiceLifetime.Scoped"/>.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="lifetime">The service lifetime.</param>
    /// <exception cref="InvalidServiceLifetimeException"></exception>
    internal static void ValidLifetimeForView(Type view, ServiceLifetime lifetime)
    {
        if (lifetime is ServiceLifetime.Scoped)
            throw new InvalidServiceLifetimeException(view, lifetime);
    }
}
