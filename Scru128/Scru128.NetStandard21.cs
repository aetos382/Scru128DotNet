#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER

using System;

namespace Scru128;

public readonly partial struct Scru128
{
    public Scru128(
        ReadOnlySpan<byte> bytes)
    {
        throw new NotImplementedException();
    }
    
    public static Scru128 Parse(
        ReadOnlySpan<char> input)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(
        ReadOnlySpan<char> input,
        out Scru128 result)
    {
        throw new NotImplementedException();
    }

    public static bool TryWriteBytes(
        Span<byte> destination)
    {
        throw new NotImplementedException();
    }
}

#endif
