using System;

#if NETSTANDARD2_0
using System.Buffers;
#endif

using System.Security.Cryptography;

namespace Scru128DotNet;

public sealed class Scru128Generator :
    IDisposable
{
    public Scru128 Generate()
    {
        return this.Generate(
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    }

    public Scru128 Generate(
        long timestamp)
    {
        Argument.ValidateTimestamp(timestamp);

        this.CheckDisposed();

        long ts = this._timestamp;
        int hi = this._counterHigh;
        int lo = this._counterLow;
        long tsHi = this._timestampWhenCounterHighUpdated;

        if (timestamp > ts)
        {
            // counter_lo is reset to a random number whenever timestamp moves forward.
            ts = timestamp;
            lo = this.NextInt() & Scru128.MaxCounter;
        }
        else if (timestamp + 10000 > ts)
        {
            // same timestamp or the rollback is small enough (e.g. a few seconds)
            ++lo;

            if (lo > Scru128.MaxCounter)
            {
                // when counter_lo reaches its maximum value, counter_hi is incremented and counter_lo is reset to zero.
                lo = 0;
                ++hi;

                if (hi > Scru128.MaxCounter)
                {
                    hi = 0;
                    ++ts;
                    lo = this.NextInt() & Scru128.MaxCounter;
                }
            }
        }
        else
        {
            // reset state if clock moves back by ten seconds or more
            tsHi = 0;
            ts = timestamp;
            lo = this.NextInt() & Scru128.MaxCounter;
        }

        if (ts - tsHi >= 1000)
        {
            // counter_hi is reset to a random number when timestamp has moved forward by one second or more since the last renewal of counter_hi.
            tsHi = ts;
            hi = this.NextInt() & Scru128.MaxCounter;
        }

        this._timestamp = ts;
        this._counterHigh = hi;
        this._counterLow = lo;
        this._timestampWhenCounterHighUpdated = tsHi;

        var result = new Scru128(
            ts,
            hi,
            lo,
            this.NextInt());

        return result;
    }

    public void Dispose()
    {
        if (this._disposed)
        {
            return;
        }

        this._disposed = true;
        this._randomNumberGenerator.Dispose();
    }

    private int NextInt()
    {
#if NETSTANDARD2_0
        var pool = ArrayPool<byte>.Shared;
        var buffer = pool.Rent(4);

        this._randomNumberGenerator.GetBytes(buffer);
        int result = BitConverter.ToInt32(buffer, 0);

        pool.Return(buffer);

        return result;
#else
        Span<byte> buffer = stackalloc byte[4];
        this._randomNumberGenerator.GetBytes(buffer);
        return BitConverter.ToInt32(buffer);
#endif
    }

    private readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

    private long _timestamp;
    private int _counterHigh;
    private int _counterLow;
    private long _timestampWhenCounterHighUpdated;

    private bool _disposed;

    private void CheckDisposed()
    {
        if (this._disposed)
        {
            throw new ObjectDisposedException(this.GetType().Name);
        }
    }
}
