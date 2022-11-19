namespace Scru128.Tests;

public class PropertyTest
{
    [Fact]
    public void ReadTimestamp()
    {
        var scru128 = new Scru128(0x0000_1234_5678_9abc, 0, 0, 0);

        Assert.Equal(0x0000_1234_5678_9abc, scru128.Timestamp);
        Assert.Equal(0, scru128.CounterHigh);
        Assert.Equal(0, scru128.CounterLow);
        Assert.Equal(0, scru128.Entropy);
    }

    [Fact]
    public void ReadCounterHigh()
    {
        var scru128 = new Scru128(0, 0x0012_3456, 0, 0);

        Assert.Equal(0, scru128.Timestamp);
        Assert.Equal(0x0012_3456, scru128.CounterHigh);
        Assert.Equal(0, scru128.CounterLow);
        Assert.Equal(0, scru128.Entropy);
    }

    [Fact]
    public void ReadCounterLow()
    {
        var scru128 = new Scru128(0, 0, 0x0012_3456, 0);

        Assert.Equal(0, scru128.Timestamp);
        Assert.Equal(0, scru128.CounterHigh);
        Assert.Equal(0x0012_3456, scru128.CounterLow);
        Assert.Equal(0, scru128.Entropy);
    }

    [Fact]
    public void ReadEntropy()
    {
        var scru128 = new Scru128(0, 0, 0, 0x1234_5678);

        Assert.Equal(0, scru128.Timestamp);
        Assert.Equal(0, scru128.CounterHigh);
        Assert.Equal(0, scru128.CounterLow);
        Assert.Equal(0x1234_5678, scru128.Entropy);
    }
}
