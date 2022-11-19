namespace Scru128DotNet.Tests;

public class FormattingTest
{
    [Theory]
    // ReSharper disable StringLiteralTypo
    [InlineData(0, 0, 0, 0, "0000000000000000000000000")]
    [InlineData(MAX_INT48, 0, 0, 0, "F5LXX1ZZ5K6TP71GEEH2DB7K0")]
    [InlineData(0, MAX_INT24, 0, 0, "0000000005GV2R2KJWR7N8XS0")]
    [InlineData(0, 0, MAX_INT24, 0, "00000000000000JPIA7QL4HS0")]
    [InlineData(0, 0, 0, MAX_INT32, "0000000000000000001Z141Z3")]
    [InlineData(MAX_INT48, MAX_INT24, MAX_INT24, MAX_INT32, "F5LXX1ZZ5PNORYNQGLHZMSP33")]
    // ReSharper restore StringLiteralTypo
    public void ToStringTest(
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
