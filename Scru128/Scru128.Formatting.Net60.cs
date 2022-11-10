#if NET6_0_OR_GREATER

using System;

namespace Scru128;

public readonly partial struct Scru128 :
    ISpanFormattable
{
    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        var result = this.TryFormat(destination);
        charsWritten = result ? CharCount : 0;
        return result;
    }
}

#endif
