﻿#if NETSTANDARD2_0

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

#endif
