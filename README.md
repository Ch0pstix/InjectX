# InjectX · ![License](https://img.shields.io/github/license/Ch0pstix/InjectX?style=flat-square)

![InjectX Banner](/res/banner.png?raw=true)

Automatic service registration extensions for `Microsoft.Extensions.DependencyInjection`.

## 🚀 Getting Started

The packages may be installed via Nuget, Package Manager Console, or DotNet CLI.

### Nuget

- [Ch0pstix.InjectX][1]

  ![Version](https://img.shields.io/nuget/v/Ch0pstix.InjectX?label=Version&style=flat-square) ![Downloads](https://img.shields.io/nuget/dt/Ch0pstix.InjectX?label=Downloads&style=flat-square)

- [Ch0pstix.InjectX.Mvvm][2]
  
  ![Version](https://img.shields.io/nuget/v/Ch0pstix.InjectX.Mvvm?label=Version&style=flat-square) ![Downloads](https://img.shields.io/nuget/dt/Ch0pstix.InjectX.Mvvm?label=Downloads&style=flat-square)

[1]: https://www.nuget.org/packages/Ch0pstix.InjectX/
[2]: https://www.nuget.org/packages/Ch0pstix.InjectX.Mvvm/

### Package Manager Console

```shell
Install-Package Ch0pstix.InjectX
Install-Package Ch0pstix.InjectX.Mvvm
```

### DotNet CLI

```shell
dotnet add package Ch0pstix.InjectX
dotnet add package Ch0pstix.InjectX.Mvvm
```

## 📂 What's Included

### Types

| Name                                                           | Description                                                                                                    |
| -------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| [SingletonAttribute](src/InjectX.Shared/SingletonAttribute.cs) | Specifies that a view or service should be registered with a `ServiceLifetime` of `ServiceLifetime.Singleton`. |
| [TransientAttribute](src/InjectX.Shared/TransientAttribute.cs) | Specifies that a view or service should be registered with a `ServiceLifetime` of `ServiceLifetime.Transient`. |
| [ScopedAttribute](src/InjectX.Shared/ScopedAttribute.cs)       | Specifies that a service should be registered with a `ServiceLifetime` of `ServiceLifetime.Scoped`.            |
| [RegistrationStrategy](src/InjectX/RegistrationStrategy.cs)    | Specifies strategies that may be applied when adding a `ServiceDescriptor` to an `IServiceCollection`.         |

### Extension Methods

| Method                                                                                                                                     | Description                                                                                    |
| ------------------------------------------------------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------------------------------- |
| [IServiceCollection.RegisterApplicationServices(optional: ServiceRegistrationStrategy)](src/InjectX/ServiceCollectionExtensions.cs)        | Registers service objects that have been defined within the application's assembly.            |
| [IServiceCollection.RegisterAssemblyServices(Assembly, optional: ServiceRegistrationStrategy)](src/InjectX/ServiceCollectionExtensions.cs) | Registers service objects that have been defined within the specified assembly.                |
| [IServiceCollection.RegisterViewsAndViewModels()](src/InjectX.Mvvm/ServiceCollectionExtensions.cs)                                         | Registers view and viewmodel objects that have been defined within the application's assembly. |

## 💡 Usage Examples

### Console App (InjectX)

#### MyConsoleApp/Program.cs
```csharp
using MyConsoleApp.Services;

var services = new ServiceCollection()
  .RegisterApplicationServices() // from MyConsoleApp.Services*
  .BuildServiceProvider();

var service = services.GetRequiredService<IExampleService>();

service.SayHello();
```

#### MyConsoleApp/Services/IExampleService.cs
```csharp
public interface IExampleService()
{
  void SayHello();
}
```

#### MyConsoleApp/Services/ExampleService.cs
```csharp
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

### Wpf Mvvm App (InjectX + InjectX.Mvvm)

#### MyWpfApp/App.xaml.cs:
```csharp
public partial class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    var services = new ServiceCollection()
      .RegisterApplicationServices()  // from MyWpfApp.Services*
      .RegisterViewsAndViewModels() // from MyWpfApp.Views* && MyWpfApp.ViewModels*
      .BuildServiceProvider();
      
    var mainWindow = services.GetRequiredService<MainWindow>();
    
    mainWindow.Show();
    
    base.OnStartup(e);
  }
}
```

#### MyWpfApp/Views/Dialogs/MyCustomDialogWindow.cs:
```csharp
[Transient] // Override the default singleton lifetime as dialogs tend to be reusable objects
public partial class MyCustomDialogWindow : Window
{
  public MyCustomDialogWindow()
  {
    InitializeComponent();
  }
}
```
