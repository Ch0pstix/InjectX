namespace InjectX.Internals;

[DebuggerStepThrough]
internal static class ReflectionExtensions
{
    internal static string GetRootNamespace(this Assembly assembly)
    {
        return assembly.EntryPoint?.DeclaringType?.Namespace
            ?? assembly.GetName().Name
            ?? throw new InvalidOperationException(SR.NamespaceResolutionFailure.Format(assembly));
    }

    internal static string GetDisplayName(this Type type)
    {
        return TypeNameHelper.GetTypeDisplayName(type, includeGenericParameters: true);
    }

    internal static bool IsInNamespace(this Type type, string ns, bool rootOnly = false)
    {
        string typeNs = type.Namespace ?? string.Empty;

        if (typeNs.Length < ns.Length)
            return false;

        return rootOnly
            ? typeNs.Equals(ns, StringComparison.Ordinal)
            : typeNs.StartsWith(ns, StringComparison.Ordinal);
    }

    internal static bool HasAttribute(this Type type, Type attributeType)
    {
        return type.IsDefined(attributeType, true);
    }

    internal static bool HasAttribute<T>(this Type type)
        where T : Attribute
    {
        return type.HasAttribute(typeof(T));
    }

    internal static bool IsUserDefinedType(this Type type)
    {
        return !type.HasAttribute<CompilerGeneratedAttribute>();
    }

    internal static bool IsBasedOn(this Type type, Type candidate)
    {
        return candidate.IsAssignableFrom(type);
    }

    internal static bool IsAssignableToGenericTypeDefinition(this Type type, Type genericType)
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
}
