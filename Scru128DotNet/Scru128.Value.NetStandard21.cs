#if NETSTANDARD2_1_OR_GREATER

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Scru128DotNet;

public readonly partial struct Scru128
{
    [StructLayout(LayoutKind.Sequential, Size = BytesCount)]
    private partial struct Value
    {
        public static implicit operator Span<byte>(
            in Value value)
        {
            return MemoryMarshal.CreateSpan(
                ref Unsafe.AsRef(in value._byte0),
                BytesCount);
        }

        public static implicit operator ReadOnlySpan<byte>(
            in Value value)
        {
            return MemoryMarshal.CreateReadOnlySpan(
                ref Unsafe.AsRef(in value._byte0),
                BytesCount);
        }
    }
}

#endif
