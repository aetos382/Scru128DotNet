#if NETSTANDARD2_0

#pragma warning disable IDE0130

namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class NotNullAttribute :
    Attribute
{
}

#pragma warning restore

#endif
