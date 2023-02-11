![InjectX Banner][1]
![Build Workflow Status - Master][2] ![CodeQL Workflow Status - Master][3]

# What is InjectX? 🔎

The **InjectX** and **InjectX.Mvvm** libraries were developed with the aim of simplifying the implementation of dependency injection in your .NET applications. They achieve this by eliminating the need to register services, and in the case of **InjectX.Mvvm**, views and viewmodels, individually with the service container.

This is made possible by adding [a few helpful extensions][4] to the `IServiceCollection` class from Microsoft's dependency injection library, [Microsoft.Extensions.DependencyInjection.][5]


# Getting started 🚀

> **Note**: The dev prefix **Ch0pstix.\*** is added to the package IDs as a means of avoiding naming conflicts with some unlisted packages in the NuGet catalog. This prefix is not present in the namespaces of the actual libraries.

The packages may be installed via nuget, package manager console, or dotnet cli.

### NuGet

- [Ch0pstix.InjectX][6]
  
  ![Ch0pstix.InjectX Nuget Version][7] ![Ch0pstix.InjectX Nuget Downloads][8]

- [Ch0pstix.InjectX.Mvvm][9]
  
  ![Ch0pstix.InjectX.Mvvm Nuget Version][10] ![Ch0pstix.InjectX.Mvvm Nuget Downloads][11]

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


# What's included 📂

### Types

| Name                       | Description                                                                                                    |
| :------------------------- | :------------------------------------------------------------------------------------------------------------- |
| [RegistrationStrategy][12] | Specifies strategies that may be applied when adding a `ServiceDescriptor` to an `IServiceCollection`.         |

### Extensions

| Method                            | Description                                                                                    |
| :-------------------------------- | :--------------------------------------------------------------------------------------------- |
| [RegisterApplicationServices][13] | Registers service objects that have been defined within the application's assembly.            |
| [RegisterAssemblyServices][13]    | Registers service objects that have been defined within the specified assembly.                |
| [RegisterViewsAndViewModels][14]  | Registers view and viewmodel objects that have been defined within the application's assembly. |

### Attributes

| Name                       | Description                                                                                                    |
| :------------------------- | :------------------------------------------------------------------------------------------------------------- |
| [SingletonAttribute][15]   | Specifies that a view or service should be registered with a `ServiceLifetime` of `ServiceLifetime.Singleton`. |
| [TransientAttribute][16]   | Specifies that a view or service should be registered with a `ServiceLifetime` of `ServiceLifetime.Transient`. |
| [ScopedAttribute][17]      | Specifies that a service should be registered with a `ServiceLifetime` of `ServiceLifetime.Scoped`.            |


# Usage examples 🪄

## Console App (InjectX)

#### Setting up the application service provider:

```csharp
using MyConsoleApp.Services;

var services = new ServiceCollection()
  .RegisterApplicationServices() // from MyConsoleApp.Services
  .BuildServiceProvider();

var service = services.GetRequiredService<IExampleService>();

service.SayHello();
```

#### Setting a service's lifetime via lifetime annotation:

```csharp
[Transient] // Defaults to transient anyways when not provided.
public class ExampleService : IExampleService
{
  public void SayHello()
  {
    Console.WriteLine("Hello World");
  }
}
```

## Wpf Mvvm App (InjectX + InjectX.Mvvm)

#### Setting up the application service provider:

```csharp
public partial class App : Application
{
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
}
```

#### Overriding a view's lifetime via lifetime annotation:

```csharp
[Transient] // Override the default singleton lifetime (for views inheriting Window) since dialogs tend to be reused
public partial class MyCustomDialogWindow : Window
{
  public MyCustomDialogWindow()
  {
    InitializeComponent();
  }
}
```


# License 📄

**InjectX and related libraries** are free and open source software under the **MIT License**. This license permits the modification, distribution, and private and commercial use of this software. Keep in mind that you must include a copy of this license in your projects. 

[View LICENSE.txt][18]


<!-- References -->

[1]: res/banner.png?raw=true
[2]: https://img.shields.io/github/actions/workflow/status/Ch0pstix/InjectX/build-master.yml?branch=master&label=build&logo=github&style=flat-square
[3]: https://img.shields.io/github/actions/workflow/status/Ch0pstix/InjectX/codeql-master.yml?branch=master&label=codeQL&logo=github&style=flat-square
[4]: #extensions
[5]: https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection
[6]: https://www.nuget.org/packages/Ch0pstix.InjectX/
[7]: https://img.shields.io/nuget/v/Ch0pstix.InjectX?label=version&logo=nuget&style=flat-square
[8]: https://img.shields.io/nuget/dt/Ch0pstix.InjectX?logo=nuget&style=flat-square
[9]: https://www.nuget.org/packages/Ch0pstix.InjectX.Mvvm/
[10]: https://img.shields.io/nuget/v/Ch0pstix.InjectX.Mvvm?label=version&logo=nuget&style=flat-square
[11]: https://img.shields.io/nuget/dt/Ch0pstix.InjectX.Mvvm?logo=nuget&style=flat-square
[12]: src/InjectX/RegistrationStrategy.cs
[13]: src/InjectX/ServiceCollectionExtensions.cs
[14]: src/InjectX.Mvvm/ServiceCollectionExtensions.cs
[15]: src/InjectX.Shared/SingletonAttribute.cs
[16]: src/InjectX.Shared/TransientAttribute.cs
[17]: src/InjectX.Shared/ScopedAttribute.cs
[18]: LICENSE.txt
