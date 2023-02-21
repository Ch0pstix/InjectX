namespace InjectX;

/// <summary>
/// The exception that is thrown when adding a duplicate <see cref="ServiceDescriptor"/> to an <see cref="IServiceCollection"/>.
/// </summary>
public sealed class DuplicateServiceException : Exception
{
    /// <summary>
    /// Initializes an instance of the <see cref="DuplicateServiceException"/> class.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> containing the existing descriptor.</param>
    /// <param name="descriptor">The <see cref="ServiceDescriptor"/> of the duplicate service.</param>
    /// <param name="serviceCollectionName"></param>
    internal DuplicateServiceException(IServiceCollection serviceCollection, ServiceDescriptor descriptor, [CallerArgumentExpression(nameof(serviceCollection))] string serviceCollectionName = "")
        : base($"{serviceCollectionName} already contains a descriptor with a service type of '{descriptor.ServiceType}'.")
    {
        ServiceCollection = serviceCollection;
        Descriptor = descriptor;
    }

    /// <summary>
    /// The <see cref="IServiceCollection"/> containing the existing descriptor.
    /// </summary>
    public IServiceCollection ServiceCollection { get; }

    /// <summary>
    /// The <see cref="ServiceDescriptor"/> of the duplicate service.
    /// </summary>
    public ServiceDescriptor Descriptor { get; }
}
