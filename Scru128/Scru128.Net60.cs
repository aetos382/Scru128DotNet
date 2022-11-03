using System;

namespace Scru128;

#if NET6_0_OR_GREATER

public readonly partial struct Scru128 :
    ISpanFormattable
{
    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }
}

#endif