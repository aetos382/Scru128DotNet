#if NETSTANDARD2_0_OR_GREATER

#pragma warning disable IDE0130

namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class CallerArgumentExpressionAttribute :
    Attribute
{
    public CallerArgumentExpressionAttribute(
        string parameterName)
    {
        this.ParameterName = parameterName;
    }

    public string ParameterName { get; }
}

#pragma warning restore IDE0130

#endif
