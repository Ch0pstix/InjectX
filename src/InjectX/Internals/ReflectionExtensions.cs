namespace InjectX.Internals;

[DebuggerStepThrough]
internal static class ReflectionExtensions
{
    internal static string GetRootNamespace(this Assembly assembly)
    {
        // Try to get the root namespace from the entry point type first to handle
        // cases where the assembly naming differs from the namespace structure.
        return assembly.EntryPoint?.DeclaringType?.Namespace
            ?? assembly.GetName().Name;
    }

    internal static IEnumerable<Type> GetBaseTypes(this Type type)
    {
        foreach (Type contract in type.GetInterfaces())
            yield return contract;

        Type baseType = type.BaseType;

        while (baseType is not null)
        {
            yield return baseType;
            baseType = baseType.BaseType;
        }
    }

    internal static string GetDisplayName(this Type type)
    {
        return TypeNameHelper.GetTypeDisplayName(type, includeGenericParameters: true);
    }

    internal static bool HasAttribute(this Type type, Type attributeType)
    {
        return type.IsDefined(attributeType, inherit: true);
    }

    internal static bool HasAttribute<T>(this Type type)
        where T : Attribute
    {
        return type.HasAttribute(typeof(T));
    }

    internal static bool IsInNamespace(this Type type, string @namespace, bool rootOnly = false)
    {
        string typeNamespace = type.Namespace ?? string.Empty;

        if (typeNamespace.Length < @namespace.Length)
            return false;

        return rootOnly
            ? typeNamespace.Equals(@namespace, StringComparison.Ordinal)
            : typeNamespace.StartsWith(@namespace, StringComparison.Ordinal);
    }

    internal static bool IsAbstractOrInterface(this Type type)
    {
        return type.IsClass && !type.IsAbstract;
    }

    internal static bool IsBasedOn(this Type type, Type otherType)
    {
        return otherType.IsGenericTypeDefinition
            ? type.IsAssignableToGenericTypeDefinition(otherType)
            : otherType.IsAssignableFrom(type);
    }

    private static bool IsAssignableToGenericTypeDefinition(this Type type, Type genericType)
    {
        foreach (Type contract in type.GetInterfaces())
        {
            if (contract.IsGenericType)
            {
                Type genericTypeDefinition = contract.GetGenericTypeDefinition();
                if (genericTypeDefinition == genericType) return true;
            }
        }

        if (type.IsGenericType)
        {
            Type genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == genericType) return true;
        }

        return type.BaseType is Type baseType 
            && baseType.IsAssignableToGenericTypeDefinition(genericType);
    }
}
