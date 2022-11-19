#if NET7_0_OR_GREATER

using System;
using System.Diagnostics.CodeAnalysis;

namespace Scru128DotNet;

#pragma warning disable CA1305

public readonly partial struct Scru128 :
    ISpanParsable<Scru128>
{
    public static Scru128 Parse(
        string s,
        IFormatProvider? provider)
    {
        Argument.NotNull(s);

        return Parse(s.AsSpan());
    }

    public static Scru128 Parse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider)
    {
        return Parse(s);
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out Scru128 result)
    {
        return TryParse(s.AsSpan(), out result);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        out Scru128 result)
    {
        return TryParse(s, out result);
    }
}

#pragma warning restore CA1305

#endif
