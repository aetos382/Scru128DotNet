#if NETSTANDARD2_0

#pragma warning disable IDE0130

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

#pragma warning restore

#endif
