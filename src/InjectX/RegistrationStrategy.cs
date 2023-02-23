namespace InjectX;

/// <summary>
/// Defines strategies that may be applied when adding a <see cref="ServiceDescriptor"/> to an <see cref="IServiceCollection"/>.
/// </summary>
public abstract class RegistrationStrategy
{
    /// <summary>
    /// Adds <paramref name="descriptor"/> to the <see cref="IServiceCollection"/> using the registration logic defined by the <see cref="RegistrationStrategy"/>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="descriptor">The descriptor.</param>
    internal abstract void Execute(IServiceCollection services, ServiceDescriptor descriptor);

    /// <summary>
    /// Registers new service descriptors to the the <see cref="IServiceCollection"/>, regardless of existing descriptors with the same <see cref="ServiceDescriptor.ServiceType"/>.
    /// </summary>
    public static RegistrationStrategy Append { get; } = new AppendStrategy();

    /// <summary>
    /// Registers new service descriptors to the <see cref="IServiceCollection"/>, replacing existing descriptors with the same <see cref="ServiceDescriptor.ServiceType"/>.
    /// </summary>
    public static RegistrationStrategy Replace { get; } = new ReplaceStrategy();

    /// <summary>
    /// Registers new service descriptors to the <see cref="IServiceCollection"/>, only when there are no existing descriptors with the same <see cref="ServiceDescriptor.ServiceType"/>.
    /// </summary>
    public static RegistrationStrategy Skip { get; } = new SkipStrategy();

    /// <summary>
    /// Registers new service descriptors to the <see cref="IServiceCollection"/>, throwing exeptions when existing descriptors with the same <see cref="ServiceDescriptor.ServiceType"/> are found.
    /// </summary>
    public static RegistrationStrategy Throw { get; } = new ThrowStrategy();

    /// <summary>
    /// Implements the strategy logic for <see cref="Append"/>.
    /// </summary>
    private sealed class AppendStrategy : RegistrationStrategy
    {
        internal override void Execute(IServiceCollection services, ServiceDescriptor descriptor)
        {
            Verify.NotNull(services);
            Verify.NotNull(descriptor);

            services.Add(descriptor);
        }
    }

    /// <summary>
    /// Implements the strategy logic for <see cref="Replace"/>.
    /// </summary>
    private sealed class ReplaceStrategy : RegistrationStrategy
    {
        internal override void Execute(IServiceCollection services, ServiceDescriptor descriptor)
        {
            Verify.NotNull(services);
            Verify.NotNull(descriptor);

            services.Replace(descriptor);
        }
    }

    /// <summary>
    /// Implements the strategy logic for <see cref="Skip"/>.
    /// </summary>
    private sealed class SkipStrategy : RegistrationStrategy
    {
        internal override void Execute(IServiceCollection services, ServiceDescriptor descriptor)
        {
            Verify.NotNull(services);
            Verify.NotNull(descriptor);

            services.TryAdd(descriptor);
        }
    }

    /// <summary>
    /// Implements the strategy logic for <see cref="Throw"/>.
    /// </summary>
    private sealed class ThrowStrategy : RegistrationStrategy
    {
        internal override void Execute(IServiceCollection services, ServiceDescriptor descriptor)
        {
            Verify.NotNull(services);
            Verify.NotNull(descriptor);

            if (services.Any(d => d.ServiceType == descriptor.ServiceType))
                throw new DuplicateServiceException(services, descriptor);

            services.Add(descriptor);
        }
    }
}
