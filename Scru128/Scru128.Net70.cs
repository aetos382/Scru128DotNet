using System;
using System.Diagnostics.CodeAnalysis;

namespace Scru128;

#if NET7_0_OR_GREATER

public readonly partial struct Scru128 :
    IParsable<Scru128>
{
    public static Scru128 Parse(
        string s,
        IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Scru128 result)
    {
        throw new NotImplementedException();
    }
}

#endif
