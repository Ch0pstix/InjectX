namespace InjectX.Tests;

public class RegistrationStrategy_Tests
{
    [Fact]
    public void AppendStrategy_AddsNewServices_WhenFound()
    {
        var services = new ServiceCollection()
            .RegisterAssemblyServices(Current, RegistrationStrategy.Append);

        Assert.True(services.Any());
        Assert.Contains(services, d => d.ImplementationType == typeof(TestServiceA));
        Assert.Contains(services, d => d.ImplementationType == typeof(TestServiceB));
    }

    [Fact]
    public void SkipStrategy_SkipsNewServices_WhenDuplicateServiceTypesAreFound()
    {
        var services = new ServiceCollection()
            .RegisterAssemblyServices(Current, RegistrationStrategy.Skip); ;

        Assert.True(services.Any());
        Assert.Contains(services, d => d.ImplementationType == typeof(TestServiceA));
        Assert.DoesNotContain(services, d => d.ImplementationType == typeof(TestServiceB));
    }

    [Fact]
    public void ReplaceStrategy_ReplacesExistingServices_WhenDuplicateServiceTypesAreFound()
    {
        var services = new ServiceCollection()
            .RegisterAssemblyServices(Current, RegistrationStrategy.Replace);

        Assert.True(services.Any());
        Assert.DoesNotContain(services, d => d.ImplementationType == typeof(TestServiceA));
        Assert.Contains(services, d => d.ImplementationType == typeof(TestServiceB));
    }
    
    [Fact]
    public void ThrowStrategy_ThrowsAnException_WhenDuplicateServiceTypesAreFound()
    {
        var services = new ServiceCollection();

        Assert.Throws<DuplicateServiceException>(() => services.RegisterAssemblyServices(Current, RegistrationStrategy.Throw));
    }

    private readonly Assembly Current = typeof(ITestService).Assembly;
}
