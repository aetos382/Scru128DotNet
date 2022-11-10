#if NET7_0_OR_GREATER

using System;
using System.Diagnostics.CodeAnalysis;

namespace Scru128;

public readonly partial struct Scru128 :
    ISpanParsable<Scru128>
{
    public static Scru128 Parse(
        string input,
        IFormatProvider? provider)
    {
        Argument.NotNull(input);

        return Parse(input.AsSpan(), provider);
    }

    public static Scru128 Parse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider)
    {
        return Parse(s);
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? input,
        IFormatProvider? provider,
        out Scru128 result)
    {
        return TryParse(input.AsSpan(), out result);
    }

    public static bool TryParse(
        ReadOnlySpan<char> input,
        IFormatProvider? provider,
        out Scru128 result)
    {
        return TryParse(input, out result);
    }
}

#endif
