using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Scru128.Tests;

public partial class Scru128Test
{
    /*
#if NET7_0_OR_GREATER

    [GeneratedRegex("^[0-9A-Z]{25}", RegexOptions.Singleline | RegexOptions.CultureInvariant)]
    private static partial Regex Scru128Pattern();

#else

    private static readonly Regex _pattern = new Regex("^[0-9A-Z]{25}", RegexOptions.Singleline | RegexOptions.CultureInvariant);

    private static Regex Scru128Pattern()
    {
        return _pattern;
    }

#endif

    private const int Count = 100;

    private readonly IReadOnlyList<string> _samples;

    public Scru128Test()
    {
        var array = new string[Count];

        for (int i = 0; i < Count; ++i)
        {
            array[i] = Scru128.GenerateString();
        }

        this._samples = array;
    }

    [Fact]
    public void TestFormat()
    {
        Assert.All(
            this._samples,
            static sample => Assert.Matches(Scru128Pattern(), sample));
    }

    [Fact]
    public void Compare()
    {
        for (int i = 0; i < Count - 1; ++i)
        {
            var id1 = this._samples[i];
            var id2 = this._samples[i + 1];

            Assert.True(id1.CompareTo(id2) < 0);
        }
    }
    */

    [Theory]
    [InlineData(0, 0, 0, 0, "0000000000000000000000000")]
    [InlineData(MAX_INT48, 0, 0, 0, "F5LXX1ZZ5K6TP71GEEH2DB7K0")]
    // [InlineData(0x0000_1234_5678_9abc, 0x00de_f012, 0, 0, "F5LXX1ZZ5K6TP71GEEH2DB7K0")]
    public void Test(
        long timestamp,
        int counterHigh,
        int counterLow,
        int entropy,
        string expected)
    {
        var scru128 = new Scru128(timestamp, counterHigh, counterLow, entropy);
        var actual = scru128.ToString();

        Assert.Equal(expected, actual);
    }
}
