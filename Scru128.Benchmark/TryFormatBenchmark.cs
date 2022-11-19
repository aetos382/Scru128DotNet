using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Scru128.Benchmark;

[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[MemoryDiagnoser]
public class TryFormatBenchmark
{
    [Benchmark]
    public int X()
    {
        var scru128 = new Scru128(
            0x0000_ffff_ffff_ffff,
            0x00ff_ffff,
            0x00ff_ffff,
            unchecked((int)0xffff_ffff));

        var str = scru128.ToString();

        return str.GetHashCode();
    }
}
