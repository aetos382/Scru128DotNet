using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Scru128;

public readonly partial struct Scru128
{
    public Scru128(
        byte[] bytes)
        : this(
              bytes.AsSpan())
    {
    }

    public Scru128(
        ReadOnlySpan<byte> bytes)
    {
        Argument.ValidateByteSpan(bytes);

        bytes.CopyTo(this.AsSpan());
    }

    public Scru128(
        long timestamp,
        int counterHigh,
        int counterLow,
        int entropy)
    {
        Argument.ValidateTimestamp(timestamp);
        Argument.ValidateCounter(counterHigh);
        Argument.ValidateCounter(counterLow);

        var span = this.AsSpan();

        unchecked
        {
            ulong high =
                (ulong)timestamp << 16 |
                ((ulong)counterHigh & 0x00ff_ff00) >> 8;

            ulong low =
                ((ulong)counterHigh & 0x0000_00ff) << 48 |
                (ulong)counterLow << 32 |
                (uint)entropy;

            BinaryPrimitives.WriteUInt64BigEndian(span, high);
            BinaryPrimitives.WriteUInt64BigEndian(span.Slice(8), low);
        }
    }

    public byte[] ToByteArray()
    {
        return this.AsReadOnlySpan().ToArray();
    }

    public bool TryWriteBytes(
        Span<byte> destination)
    {
        return this.AsReadOnlySpan().TryCopyTo(destination);
    }

    private static readonly Scru128Generator _defaultGenerator = new Scru128Generator();

    public static Scru128 Generate()
    {
        return _defaultGenerator.Generate();
    }

    public static string GenerateString()
    {
        return Generate().ToString();
    }

    private unsafe struct Value
    {
        public fixed byte Values[16];
    }

    private readonly Value _value;

    private unsafe Span<byte> AsSpan()
    {
        fixed (byte* p = this._value.Values)
        {
            return new Span<byte>(p, BytesCount);
        }
    }

    private unsafe ReadOnlySpan<byte> AsReadOnlySpan()
    {
        fixed (byte* p = this._value.Values)
        {
            return new ReadOnlySpan<byte>(p, BytesCount);
        }
    }

    private long SubLong(
        int beginIndex,
        int endIndex)
    {
        long result = 0;
        var span = this.AsReadOnlySpan();

        for (var i = beginIndex; i < endIndex; ++i)
        {
            result = (result << 8) | span[i];
        }

        return result;
    }

    internal const int BytesCount = 16;
    internal const int CharCount = 25;
    internal const long MaxTimestamp = 0x0000_ffff_ffff_ffff;
    internal const int MaxCounter = 0x00ff_ffff;
}
