using System;
using System.Buffers.Binary;

namespace Scru128DotNet;

public readonly partial struct Scru128 :
    IFormattable
{
    public override string ToString()
    {
#if NETSTANDARD2_0
        Span<char> buffer = stackalloc char[CharCount];
        this.TryFormat(buffer);

        return buffer.ToString();
#else
        string result = string.Create(CharCount, this, static (buffer, self) =>
        {
            self.TryFormat(buffer);
        });

        return result;
#endif
    }

    public string ToString(
        string? format,
        IFormatProvider? formatProvider)
    {
        return this.ToString();
    }

    public bool TryFormat(
        Span<char> destination)
    {
        if (destination.Length < CharCount)
        {
            return false;
        }

        const string DIGITS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const ulong SEVEN_BYTES = 0x00ff_ffff_ffff_ffff;

        var span = this.AsSpan();

        Span<ulong> values = stackalloc[]
        {
            BinaryPrimitives.ReadUInt16BigEndian(span),
            BinaryPrimitives.ReadUInt64BigEndian(span.Slice(1)) & SEVEN_BYTES,
            BinaryPrimitives.ReadUInt64BigEndian(span.Slice(8)) & SEVEN_BYTES
        };

        Span<byte> work = stackalloc byte[CharCount];

        for (int i = 0, minIndex = CharCount + 1; i < 3; ++i)
        {
            ulong carry = values[i];
            int j = CharCount - 1;

            for (; carry != 0 || j > minIndex; --j)
            {
                ref byte rw = ref work[j];
                byte w = rw;

                if (w != 0)
                {
                    carry += (ulong)w << 56;
                }

                DivRem36(ref carry, out rw);
            }

            minIndex = j;
        }

        for (int i = 0; i < CharCount; ++i)
        {
            destination[i] = DIGITS[work[i]];
        }

        return true;
    }

    private static void DivRem36(
        ref ulong value,
        out byte remainder)
    {
        unchecked
        {
#if NET6_0_OR_GREATER
            (value, ulong r) = Math.DivRem(value, Radix);
            remainder = (byte)r;
#else
            ulong quotient = value / Radix;
            remainder = (byte)(value - (quotient * Radix));
            value = quotient;
#endif
        }
    }
}
