using System;

namespace Scru128DotNet;

public readonly partial struct Scru128
{
    public static Scru128 Parse(
        string input)
    {
        Argument.NotNull(input);

        if (!TryParse(input.AsSpan(), out var result))
        {
            throw new FormatException();
        }

        return result;
    }

    public static Scru128 Parse(
        ReadOnlySpan<char> input)
    {
        if (!TryParse(input, out var result))
        {
            throw new FormatException();
        }

        return result;
    }

    public static bool TryParse(
        string input,
        out Scru128 result)
    {
        Argument.NotNull(input);

        return TryParse(input.AsSpan(), out result);
    }

    public static bool TryParse(
        ReadOnlySpan<char> input,
        out Scru128 result)
    {
        if (input.Length != CharCount)
        {
            result = default;
            return false;
        }

        Span<byte> source = stackalloc byte[CharCount];
        for (var i = 0; i < CharCount; ++i)
        {
            var c = input[i];
            var s = DecodeMap[c];

            if (c > 'z' || s == 0x7f)
            {
                throw new ArgumentException();
            }

            source[i] = s;
        }

        Span<byte> data = stackalloc byte[BytesCount];
        var minIndex = int.MaxValue;

        for (var i = -5; i < CharCount; i += 10)
        {
            long carry = 0;

            for (var j = i < 0 ? 0 : i; j < i + 10; ++j)
            {
                carry = (carry * 36) + source[j];
            }

            var k = BytesCount - 1;
            for (; carry > 0 || k > minIndex; --k)
            {
                if (k < 0)
                {
                    throw new ArgumentException();
                }

                carry += data[k] * 3656158440062976L; // 36^10
                data[k] = (byte)carry;
                carry >>= 8;
            }

            minIndex = k;
        }

        result = new Scru128(data);
        return true;
    }

    private static readonly byte[] DecodeMap = new byte[]
    {
        0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f,
        0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f,
        0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x00, 0x01, 0x02,
        0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x0a, 0x0b, 0x0c,
        0x0d, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d,
        0x1e, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e,
        0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f,
        0x20, 0x21, 0x22, 0x23, 0x7f, 0x7f, 0x7f, 0x7f, 0x7f,
    };
}
