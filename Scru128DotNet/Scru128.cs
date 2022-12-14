using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Scru128DotNet;

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

        unchecked
        {
            ulong high =
                ((ulong)timestamp << 16) |
                (((ulong)counterHigh & 0x00ff_ff00) >> 8);

            ulong low =
                (((ulong)counterHigh & 0x0000_00ff) << 56) |
                ((ulong)counterLow << 32) |
                (uint)entropy;

            var span = this.AsSpan();

            BinaryPrimitives.WriteUInt64BigEndian(span, high);
            BinaryPrimitives.WriteUInt64BigEndian(span.Slice(8), low);
        }
    }

    public long Timestamp
    {
        get
        {
            var span = this.AsReadOnlySpan();

            long value = BinaryPrimitives.ReadInt64BigEndian(span);
            value = (value >> 16) & MaxTimestamp;

            return value;
        }
    }

    public int CounterHigh
    {
        get
        {
            var span = this.AsReadOnlySpan().Slice(6);

            int value = BinaryPrimitives.ReadInt32BigEndian(span);
            value = (value >> 8) & MaxCounter;

            return value;
        }
    }

    public int CounterLow
    {
        get
        {
            var span = this.AsReadOnlySpan().Slice(9);

            int value = BinaryPrimitives.ReadInt32BigEndian(span);
            value = (value >> 8) & MaxCounter;

            return value;
        }
    }

    public int Entropy
    {
        get
        {
            var span = this.AsReadOnlySpan().Slice(12);

            int value = BinaryPrimitives.ReadInt32BigEndian(span);
            return value;
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

    private static readonly Scru128Generator DefaultGenerator = new();

    public static Scru128 Generate()
    {
        return DefaultGenerator.Generate();
    }

    public static string GenerateString()
    {
        return Generate().ToString();
    }

    [StructLayout(LayoutKind.Sequential, Size = 16)]
    private struct Value
    {
        public byte _firstByte;
    }

    private readonly Value _value;

    private unsafe Span<byte> AsSpan()
    {
        return new Span<byte>(
            Unsafe.AsPointer(ref Unsafe.AsRef(in this._value._firstByte)),
            BytesCount);
    }

    private unsafe ReadOnlySpan<byte> AsReadOnlySpan()
    {
        return new ReadOnlySpan<byte>(
            Unsafe.AsPointer(ref Unsafe.AsRef(in this._value._firstByte)),
            BytesCount);
    }

    internal const int BytesCount = 16;
    internal const int CharCount = 25;
    internal const int Radix = 36;

    internal const long MaxTimestamp = 0x0000_ffff_ffff_ffff;
    internal const int MaxCounter = 0x00ff_ffff;
}
