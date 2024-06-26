﻿#if NET6_0_OR_GREATER

using System;

namespace Scru128DotNet;

public readonly partial struct Scru128 :
    ISpanFormattable
{
    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        bool result = this.TryFormat(destination);
        charsWritten = result ? CharCount : 0;
        return result;
    }
}

#endif
