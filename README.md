# InjectX

![InjectX Banner](/res/banner.png?raw=true)

Automatic service registration extensions for `Microsoft.Extensions.DependencyInjection`.

![License](https://img.shields.io/github/license/Ch0pstix/InjectX?style=flat)

## 🚀 Getting Started

The packages may be installed via Nuget, Package Manager Console, or DotNet CLI.

### Nuget

- [InjectX][1]

  ![Version](https://img.shields.io/nuget/v/InjectX?label=Version&style=flat) ![Downloads](https://img.shields.io/nuget/dt/InjectX?label=Downloads)

- [InjectX.Mvvm][2]
  
  ![Version](https://img.shields.io/nuget/v/InjectX.Mvvm?label=Version&style=flat) ![Downloads](https://img.shields.io/nuget/dt/InjectX.Mvvm?label=Downloads)

[1]: https://www.nuget.org/packages/InjectX/
[2]: https://www.nuget.org/packages/InjectX.Mvvm/

### Package Manager Console

```shell
Install-Package InjectX
```

```shell
Install-Package InjectX.Mvvm
```

### DotNet CLI

```shell
dotnet add package InjectX
```

```shell
dotnet add package InjectX.Mvvm
```

## 📂 What's Included

### Projects

| Name                                | Description                                                                                                                                    |
| ----------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------- |
| [InjectX](/src/InjectX)             | Contains automatic service registration extension methods for the `IServiceCollection`.                                                        |
| [InjectX.Mvvm](/src/InjectX.Mvvm)   | Contains automatic view and viewmodel registration extension methods for the `IServiceCollection`. For wpf projects using mvvm architecture.   |

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

## 🪄 Example Usage

### Console App (InjectX)

```csharp
// ConsoleApp/Program.cs

var services = new ServiceCollection()
  .RegisterApplicationServices() // from ConsoleApp.Services
  .BuildServiceProvider();

var service = services.GetRequiredService<IExampleService>();

service.DoWork();
```

```csharp
// ConsoleApp/Services/IExampleService.cs

public interface IExampleService()
{
  void DoWork();
}
```

```csharp
// ConsoleApp/Services/ExampleService.cs

// Compiled descriptor will be (IExampleService, ExampleService, ServiceLifetime.Transient)

[Transient]
public class ExampleService : IExampleService
{
  public void DoWork()
  {
    Console.WriteLine("Hello World");
  }
}
```

### WPF APP with Mvvm Architecture (InjectX.Mvvm)

```csharp
// WPFApp/App.xaml.cs

protected override void OnStartup(StartupEventArgs e)
{
  var services = new ServiceCollection()
    .RegisterApplicationServices()  // from WPFApp.Services
    .RegisterViewsAndViewModels() // from WPFApp.Views && WPFApp.ViewModels
    .BuildServiceProvider();
    
  var mainWindow = services.GetRequiredService<MainWindow>();
  
  mainWindow.Show();
  
  base.OnStartup(e);
}
```

```csharp
// WPFApp/Views/MainWindow.cs

public partial class MainWindow : Window
{
  ...
}
```

```csharp
// WPFApp/Views/CustomDialogWindow.cs

// Override the default lifetime as dialogs tend to be reusable objects

[Transient] 
public partial class CustomDialogWindow : Window
{
  ...
}
```
