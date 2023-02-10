namespace InjectX.Tests.Services;

public class TestServiceB : ITestService
{
    public void DoWork()
    {
        Debug.WriteLine($"Hello from {nameof(TestServiceB)}");
    }
}
