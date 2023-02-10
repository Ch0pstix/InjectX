namespace InjectX.Shared;

/// <summary>
/// Defines extension methods for <see cref="System.Reflection"/>.
/// </summary>
internal static class ReflectionExtensions
{
    /// <summary>
    /// Gets whether or not the inheritance tree of the type contains <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The base type</typeparam>
    /// <param name="type">The type to check</param>
    /// <returns><see langword="true"/> if <typeparamref name="T"/> is found in the inheritance tree, otherwise <see langword="false"/>.</returns>
    internal static bool IsDerivedFrom<T>(this Type type)
        where T : notnull
    {
        Verify.NotNull(type);

        Type baseType = type.BaseType;

        while (baseType is not null)
        {
            if (baseType == typeof(T))
                return true;

            baseType = baseType.BaseType;
        }

        return false;
    }

    /// <summary>
    /// Attemps to get the root namespace of <paramref name="assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>If successful, a <see cref="string"/> representing the root namespace.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    internal static string GetRootNamespace(this Assembly assembly)
    {
        Verify.NotNull(assembly);

        return assembly.EntryPoint?.DeclaringType?.Namespace
            ?? assembly.GetCommonAssemblyNamespace()
            ?? assembly.GetName().Name
            ?? throw new InvalidOperationException($"Unable to get root namespace of assembly '{assembly}'");
    }

    private static string? GetCommonAssemblyNamespace(this Assembly assembly)
    {
        IEnumerable<Type> types = assembly
            .GetTypes()
            .Where(t => t.IsPublic && t.Namespace is not null);

        if (!types.Any()) return null;

        List<string> namespaceList = new();
        types.ForEach(t => namespaceList.Add(t.Namespace));
        namespaceList = namespaceList.Distinct().ToList();

        List<(string Name, int Length)> namespaces = new();
        namespaceList.ForEach(ns => namespaces.Add((ns, ns.Split('.').Length)));

        return namespaces
            .Where(ns => ns.Length == namespaces.Select(ns => ns.Length).Min())
            .Select(ns => ns.Name)
            .SingleOrDefault();
    }
}
