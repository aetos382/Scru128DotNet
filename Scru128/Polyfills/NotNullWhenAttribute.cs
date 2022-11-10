#if NETSTANDARD2_0

namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class NotNullWhenAttribute :
    Attribute
{
    public NotNullWhenAttribute(
        bool returnValue)
    {
        this.ReturnValue = returnValue;
    }

    public bool ReturnValue { get; }
}

#endif
