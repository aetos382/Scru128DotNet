namespace Scru128DotNet.Tests;

public class ComparisonTest
{
    [Fact]
    public void 等しい()
    {
        var value1 = new Scru128(0, 0, 0, 0);
        var value2 = new Scru128(0, 0, 0, 0);

        Assert.True(value1.Equals(value2));
        Assert.True(value2.Equals(value1));

        Assert.True(value1.Equals((object)value2));
        Assert.True(value2.Equals((object)value1));

        Assert.True(value1 == value2);
        Assert.True(value2 == value1);

        Assert.False(value1 != value2);
        Assert.False(value2 != value1);
    }

    [Fact]
    public void 等しくない1()
    {
        var value1 = new Scru128(0, 0, 0, 0);
        var value2 = new Scru128(1, 0, 0, 0);

        Assert.False(value1.Equals(value2));
        Assert.False(value2.Equals(value1));

        Assert.False(value1.Equals((object)value2));
        Assert.False(value2.Equals((object)value1));

        Assert.False(value1 == value2);
        Assert.False(value2 == value1);

        Assert.True(value1 != value2);
        Assert.True(value2 != value1);
    }

    [Fact]
    public void 等しくない2()
    {
        var value1 = new Scru128(0, 0, 0, 0);
        var value2 = new Scru128(0, 1, 0, 0);

        Assert.False(value1.Equals(value2));
        Assert.False(value2.Equals(value1));

        Assert.False(value1.Equals((object)value2));
        Assert.False(value2.Equals((object)value1));

        Assert.False(value1 == value2);
        Assert.False(value2 == value1);

        Assert.True(value1 != value2);
        Assert.True(value2 != value1);
    }

    [Fact]
    public void 等しくない3()
    {
        var value1 = new Scru128(0, 0, 0, 0);
        var value2 = new Scru128(0, 0, 1, 0);

        Assert.False(value1.Equals(value2));
        Assert.False(value2.Equals(value1));

        Assert.False(value1.Equals((object)value2));
        Assert.False(value2.Equals((object)value1));

        Assert.False(value1 == value2);
        Assert.False(value2 == value1);

        Assert.True(value1 != value2);
        Assert.True(value2 != value1);
    }

    [Fact]
    public void 等しくない4()
    {
        var value1 = new Scru128(0, 0, 0, 0);
        var value2 = new Scru128(0, 0, 0, 1);

        Assert.False(value1.Equals(value2));
        Assert.False(value2.Equals(value1));

        Assert.False(value1.Equals((object)value2));
        Assert.False(value2.Equals((object)value1));

        Assert.False(value1 == value2);
        Assert.False(value2 == value1);

        Assert.True(value1 != value2);
        Assert.True(value2 != value1);
    }
}
