# InjectX · ![License](https://img.shields.io/github/license/Ch0pstix/InjectX?style=flat-square)

![InjectX Banner](/res/banner.png?raw=true)

Automatic service registration extensions for `Microsoft.Extensions.DependencyInjection`.

## 🚀 Getting Started

The packages may be installed via Nuget, Package Manager Console, or DotNet CLI.

### Nuget

- [InjectX][1]

  ![Version](https://img.shields.io/nuget/v/InjectX?label=Version&style=flat-square) ![Downloads](https://img.shields.io/nuget/dt/InjectX?label=Downloads&style=flat-square)

- [InjectX.Mvvm][2]
  
  ![Version](https://img.shields.io/nuget/v/InjectX.Mvvm?label=Version&style=flat-square) ![Downloads](https://img.shields.io/nuget/dt/InjectX.Mvvm?label=Downloads&style=flat-square)

[1]: https://www.nuget.org/packages/InjectX/
[2]: https://www.nuget.org/packages/InjectX.Mvvm/

### Package Manager Console

```shell
Install-Package InjectX
Install-Package InjectX.Mvvm
```

### DotNet CLI

```shell
dotnet add package InjectX
dotnet add package InjectX.Mvvm
```

## 📂 What's Included

### Types

| Name                                                                      | Description                                                                                                    |
| ------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| [SingletonAttribute](src/InjectX.Shared/SingletonAttribute.cs)            | Specifies that a view or service should be registered with a `ServiceLifetime` of `ServiceLifetime.Singleton`. |
| [TransientAttribute](src/InjectX.Shared/TransientAttribute.cs)            | Specifies that a view or service should be registered with a `ServiceLifetime` of `ServiceLifetime.Transient`. |
| [ScopedAttribute](src/InjectX.Shared/ScopedAttribute.cs)                  | Specifies that a service should be registered with a `ServiceLifetime` of `ServiceLifetime.Scoped`.            |
| [ServiceRegistrationStrategy](src/InjectX/ServiceRegistrationStrategy.cs) | Specifies strategies that may be applied when adding a `ServiceDescriptor` to an `IServiceCollection`.         |

### Extension Methods

| Method                                                                                                                                     | Description                                                                                    |
| ------------------------------------------------------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------------------------------- |
| [IServiceCollection.RegisterApplicationServices(optional: ServiceRegistrationStrategy)](src/InjectX/ServiceCollectionExtensions.cs)        | Registers service objects that have been defined within the application's assembly.            |
| [IServiceCollection.RegisterAssemblyServices(Assembly, optional: ServiceRegistrationStrategy)](src/InjectX/ServiceCollectionExtensions.cs) | Registers service objects that have been defined within the specified assembly.                |
| [IServiceCollection.RegisterViewsAndViewModels()](src/InjectX.Mvvm/ServiceCollectionExtensions.cs)                                         | Registers view and viewmodel objects that have been defined within the application's assembly. |

## 💡 Usage Examples

### Console App (InjectX)

```csharp
// MyConsoleApp/Program.cs

var services = new ServiceCollection()
  .RegisterApplicationServices() // from MyConsoleApp.Services
  .BuildServiceProvider();

var service = services.GetRequiredService<IExampleService>();

service.SayHello();
```

```csharp
// MyConsoleApp/Services/IExampleService.cs

public interface IExampleService()
{
  void SayHello();
}
```

```csharp
// MyConsoleApp/Services/ExampleService.cs

[Transient] // Compiled descriptor will be (IExampleService, ExampleService, ServiceLifetime.Transient)
public class ExampleService : IExampleService
{
  public void SayHello()
  {
    Console.WriteLine("Hello World");
  }
}
```

---

### Wpf App (InjectX.Mvvm)

```csharp
// MyWpfApp/App.xaml.cs

protected override void OnStartup(StartupEventArgs e)
{
  var services = new ServiceCollection()
    .RegisterApplicationServices()  // from MyWpfApp.Services
    .RegisterViewsAndViewModels() // from MyWpfApp.Views && MyWpfApp.ViewModels
    .BuildServiceProvider();
    
  var mainWindow = services.GetRequiredService<MainWindow>();
  
  mainWindow.Show();
  
  base.OnStartup(e);
}
```

```csharp
// MyWpfApp/Views/CustomDialogWindow.cs

[Transient] // Override the default lifetime as dialogs tend to be reusable objects
public partial class CustomDialogWindow : Window
{
  public CustomDialogWindow()
  {
    InitializeComponent();
  }
}
```
