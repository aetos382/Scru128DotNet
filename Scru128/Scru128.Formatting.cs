using System;

namespace Scru128;

public readonly partial struct Scru128 :
    IFormattable
{
    public override string ToString()
    {
#if NETSTANDARD2_0
        Span<char> buffer = stackalloc char[CharCount];
        TryFormat(buffer);

        return buffer.ToString();
#else
        var result = string.Create(CharCount, this, static (buffer, self) =>
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

    private static readonly char[] Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public bool TryFormat(
        Span<char> destination)
    {
        if (destination.Length < CharCount)
        {
            return false;
        }

        Span<byte> work = stackalloc byte[CharCount];
        var minIndex = int.MaxValue;

        for (int i = -5; i < 16; i += 7)
        {
            long carry = SubLong(i < 0 ? 0 : i, i + 7);
            int j = CharCount - 1;

            for (; carry > 0 || j > minIndex; --j)
            {
                carry += (long)work[j] << 56;
                work[j] = (byte)(carry % Radix);
                carry /= Radix;
            }

            minIndex = j;
        }

        for (int i = 0; i < CharCount; ++i)
        {
            destination[i] = Digits[work[i]];
        }

        return true;
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
}
