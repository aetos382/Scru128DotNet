#if NETSTANDARD2_0

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Scru128DotNet;

public readonly partial struct Scru128
{
    [StructLayout(LayoutKind.Sequential, Size = BytesCount)]
    private partial struct Value
    {
        public static unsafe implicit operator Span<byte>(
            in Value value)
        {
            return new Span<byte>(
                Unsafe.AsPointer(
                    ref Unsafe.AsRef(in value._byte0)),
                BytesCount);
        }

        public static unsafe implicit operator ReadOnlySpan<byte>(
            in Value value)
        {
            return new ReadOnlySpan<byte>(
                Unsafe.AsPointer(
                    ref Unsafe.AsRef(in value._byte0)),
                BytesCount);
        }
    }
}

#endif
