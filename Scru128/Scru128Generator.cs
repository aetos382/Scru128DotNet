using System;

#if NETSTANDARD2_0
using System.Buffers;
#endif

using System.Security.Cryptography;

namespace Scru128;

public sealed class Scru128Generator :
    IDisposable
{
    public Scru128Generator()
        : this(
              RandomNumberGenerator.Create())
    {
    }

    public Scru128Generator(
        RandomNumberGenerator randomNumberGenerator)
    {
        Argument.NotNull(randomNumberGenerator);

        this._randomNumberGenerator = randomNumberGenerator;
    }

    public Scru128 Generate()
    {
        return Generate(
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    }

    public Scru128 Generate(
        long timestamp)
    {
        Argument.ValidateTimestamp(timestamp);

        var ts = this._timestamp;
        var hi = this._counterHigh;
        var lo = this._counterLow;

        if (timestamp > ts)
        {
            ts = timestamp;
            lo = NextInt() & Scru128.MaxCounter;
        }
        else if (timestamp + 10000 > ts)
        {
            ++lo;

            if (lo > Scru128.MaxCounter)
            {
                lo = 0;
                ++hi;

                if (hi > Scru128.MaxCounter)
                {
                    hi = 0;
                    ++ts;
                    lo = NextInt() & Scru128.MaxCounter;
                }
            }
        }
        else
        {
            this._tsCounterHi = 0;
            ts = timestamp;
            lo = NextInt() & Scru128.MaxCounter;
        }

        if (ts - _tsCounterHi >= 1000 || _tsCounterHi < 1)
        {
            _tsCounterHi = ts;
            hi = NextInt() & Scru128.MaxCounter;
        }

        (this._timestamp, this._counterHigh, this._counterLow) = (ts, hi, lo);

        var result = new Scru128(
            ts,
            hi,
            lo,
            NextInt());

        return result;
    }

    public void Dispose()
    {
        this._randomNumberGenerator.Dispose();
    }

    private int NextInt()
    {
        int result;

#if NETSTANDARD2_0
        var pool = ArrayPool<byte>.Shared;
        var buffer = pool.Rent(4);

        this._randomNumberGenerator.GetBytes(buffer);
        result = BitConverter.ToInt32(buffer, 0);

        pool.Return(buffer);
#else
        Span<byte> buffer = stackalloc byte[4];
        this._randomNumberGenerator.GetBytes(buffer);
        result = BitConverter.ToInt32(buffer);
#endif
        return result;
    }

    private readonly RandomNumberGenerator _randomNumberGenerator;

    private long _timestamp = 0;
    private int _counterHigh = 0;
    private int _counterLow = 0;
    private long _tsCounterHi = 0;
}
