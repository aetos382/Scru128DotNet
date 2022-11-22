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
        for (int i = 0; i < CharCount; ++i)
        {
            char c = input[i];

            int x = c switch
            {
                >= 'A' and <= 'Z' => c - 55,
                >= '0' and <= '9' => c - '0',
                >= 'a' and <= 'z' => c - 87,
                _ => throw new FormatException()
            };

            source[i] = unchecked((byte)x);
        }

        Span<(int Index, int Length)> indices = stackalloc[]
        {
            (0, 5),
            (5, 10),
            (15, 10)
        };

        Span<byte> data = stackalloc byte[BytesCount];

        for (int i = 0, minIndex = BytesCount + 1; i < 3; ++i)
        {
            var (index, length) = indices[i];

            var span = source.Slice(index, length);

            ulong carry = 0;

            for (int j = 0; j < length; ++j)
            {
                carry = (carry * Radix) + span[j];
            }

            int k = BytesCount - 1;

            for (; carry > 0 || k > minIndex; --k)
            {
                if (k < 0)
                {
                    throw new FormatException();
                }

                ref byte rd = ref data[k];
                byte d = rd;

                if (d != 0)
                {
                    carry += d * 3656158440062976UL; // 36^10
                }

                rd = unchecked((byte)carry);
                carry >>= 8;
            }

            minIndex = k;
        }

        result = new Scru128(data);
        return true;
    }
}
