using System.Runtime.InteropServices;

#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace Scru128DotNet;

public partial struct Scru128
{
#if NET8_0_OR_GREATER
    [InlineArray(BytesCount)]
#endif
    [StructLayout(LayoutKind.Sequential, Size = BytesCount, Pack = 1)]
    private struct ValueCore
    {
        public byte _byte0;

#if !NET8_0_OR_GREATER
        public byte _byte1;
        public byte _byte2;
        public byte _byte3;
        public byte _byte4;
        public byte _byte5;
        public byte _byte6;
        public byte _byte7;
        public byte _byte8;
        public byte _byte9;
        public byte _byte10;
        public byte _byte11;
        public byte _byte12;
        public byte _byte13;
        public byte _byte14;
        public byte _byte15;
#endif
    }

    [StructLayout(LayoutKind.Explicit, Size = BytesCount, Pack = 1)]
    private struct ValueUnion
    {
        [FieldOffset(0)] public ValueCore Core;

        [FieldOffset(0)] public ulong Value64High;

        [FieldOffset(8)] public ulong Value64Low;

#if NET7_0_OR_GREATER
        [FieldOffset(0)] public UInt128 Value128;
#endif
    }
}
