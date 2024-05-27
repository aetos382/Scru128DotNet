#if NETSTANDARD2_0_OR_GREATER

using System;

namespace Scru128DotNet;

public readonly partial struct Scru128
{
    private partial struct Value
    {
        public Span<byte> this[Range range]
        {
            get
            {
                return ((Span<byte>)this)[range];
            }
        }
    }
}

#endif
