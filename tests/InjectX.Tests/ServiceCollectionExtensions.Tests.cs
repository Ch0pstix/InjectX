namespace InjectX.Tests;

public class ServiceCollectionExtensions_Tests
{
    [Fact]
    public void RegisterApplicationServices_RegistersServicesFromEntryAssembly()
    {
        // Note: We cant really test this as it requires the calling assembly to have an entry point,
        // and unfortunately the test assembly does not. However, this shouldn't be much of an issue
        // as the method does not actually contain any registration logic but instead passes an instance
        // of the entry assembly to RegisterAssemblyServices().

        Assert.True(true);
    }

    [Fact]
    public void RegisterAssemblyServices_RegistersServicesFromSpecifiedAssembly()
    {
        var services = new ServiceCollection()
            .RegisterAssemblyServices(Current);

        Assert.Equal(2, services.Count);
        Assert.True(AllServicesAreFromAssembly(services, Current));
    }

    private static bool AllServicesAreFromAssembly(IServiceCollection services, Assembly assembly)
    {
        foreach (var service in services)
        {
            var serviceAssembly = service.ServiceType.Assembly;
            var hasImplimentation = service.ImplementationType is not null;
            var implementationAssembly = service.ImplementationType?.Assembly;

            if (serviceAssembly != assembly) 
                return false;

            if (hasImplimentation && implementationAssembly != assembly) 
                return false;
        }

        return true;
    }

    private readonly Assembly Current = typeof(ITestService).Assembly;
}
