namespace InjectX;

/// <summary>
/// Specifies that the object associated with this attribute should be skipped during service registration.<br/>
/// This class cannot be inherited.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class SkipRegistrationAttribute : Attribute { }
