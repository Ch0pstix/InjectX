namespace InjectX.Exceptions;

/// <summary>
/// The exception that is thrown when adding <see cref="ServiceDescriptor"/> to an <see cref="IServiceCollection"/> already containing the <see cref="ServiceDescriptor.ServiceType"/>.
/// </summary>
public sealed class DuplicateServiceException : InvalidOperationException
{
    /// <summary>
    /// Initializes an instance of the <see cref="DuplicateServiceException"/> class.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <param name="descriptor">The service descriptor containing the duplicate service type.</param>
    internal DuplicateServiceException(IServiceCollection serviceCollection, ServiceDescriptor descriptor)
        : base(SR.DuplicateServiceFound.Format(descriptor.ServiceType.GetDisplayName()))
    {
        ServiceCollection = serviceCollection;
        Descriptor = descriptor;
    }

    /// <summary>
    /// The service collection.
    /// </summary>
    public IServiceCollection ServiceCollection { get; }

    /// <summary>
    /// The service descriptor containing the duplicate service type.
    /// </summary>
    public ServiceDescriptor Descriptor { get; }
}
