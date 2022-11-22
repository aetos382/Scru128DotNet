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
        string actual = scru128.ToString();

        Assert.Equal(expected, actual);
    }

    // test vectors from https://github.com/scru128/spec/blob/v2.0.1/base36_128.c#L118

    [Theory]
    // ReSharper disable StringLiteralTypo
    [InlineData(new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, "0000000000000000000000000")]
    [InlineData(new byte[] {0x01, 0x7f, 0xee, 0x7f, 0xef, 0x41, 0x7e, 0x2b, 0x34, 0x32, 0xac, 0x2e, 0xc5, 0x53, 0x68, 0x7c}, "0372HG16CSMSM50L8DIKCVUKC")]
    [InlineData(new byte[] {0x01, 0x7f, 0xee, 0x7f, 0xef, 0x42, 0x7e, 0x2b, 0x34, 0x6c, 0x0f, 0xf4, 0x14, 0xbb, 0xcf, 0xfd}, "0372HG16CY3NOWRACLS909WCD")]
    [InlineData(new byte[] {0x01, 0x7f, 0xef, 0x39, 0xc2, 0x64, 0x1b, 0xa5, 0x6a, 0x94, 0x83, 0x18, 0x88, 0x41, 0xe0, 0x5a}, "0372IJOJUXUHJSFKERYI2MRTM")]
    [InlineData(new byte[] {0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff}, "F5LXX1ZZ5PNORYNQGLHZMSP33")]
    // ReSharper restore StringLiteralTypo
    public void ToStringTest2(
        byte[] data,
        string expected)
    {
        var scru128 = new Scru128(data);
        string actual = scru128.ToString();

        Assert.Equal(expected, actual);
    }
}
