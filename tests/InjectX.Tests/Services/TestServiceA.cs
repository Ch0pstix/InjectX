namespace InjectX.Tests.Services;

public class TestServiceA : ITestService
{
    public void DoWork()
    {
        Debug.WriteLine($"Hello from {nameof(TestServiceA)}");
    }
}
