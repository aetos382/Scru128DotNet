using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

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

        bytes.CopyTo(this._value);
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

            BinaryPrimitives.WriteUInt64BigEndian(this._value, high);
            BinaryPrimitives.WriteUInt64BigEndian(this._value[8..], low);
        }
    }

    public long Timestamp
    {
        get
        {
            long value = BinaryPrimitives.ReadInt64BigEndian(this._value);
            value = (value >> 16) & MaxTimestamp;

            return value;
        }
    }

    public int CounterHigh
    {
        get
        {
            int value = BinaryPrimitives.ReadInt32BigEndian(this._value[6..]);
            value = (value >> 8) & MaxCounter;

            return value;
        }
    }

    public int CounterLow
    {
        get
        {
            int value = BinaryPrimitives.ReadInt32BigEndian(this._value[9..]);
            value = (value >> 8) & MaxCounter;

            return value;
        }
    }

    public int Entropy
    {
        get
        {
            int value = BinaryPrimitives.ReadInt32BigEndian(this._value[12..]);
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

#if NET8_0_OR_GREATER
    [InlineArray(BytesCount)]
#endif
    private partial struct Value :
        IEquatable<Value>
    {
        public byte _byte0;

        public readonly bool Equals(Value other)
        {
            return this._byte0 == other._byte0;
        }

        public override bool Equals(object? obj)
        {
            return obj is Value other && Equals(other);
        }

        public override int GetHashCode()
        {
            return this._byte0.GetHashCode();
        }
    }

    private readonly Value _value;

    internal const int BytesCount = 16;
    internal const int CharCount = 25;
    internal const int Radix = 36;

    internal const long MaxTimestamp = 0x0000_ffff_ffff_ffff;
    internal const int MaxCounter = 0x00ff_ffff;
}
