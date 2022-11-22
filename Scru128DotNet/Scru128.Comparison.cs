using System;
using System.Diagnostics.CodeAnalysis;

using Scru128DotNet.Properties;

namespace Scru128DotNet;

public readonly partial struct Scru128 :
    IEquatable<Scru128>,
    IComparable<Scru128>,
    IComparable
{
    public bool Equals(
        Scru128 other)
    {
        return this.AsReadOnlySpan().SequenceEqual(other.AsReadOnlySpan());
    }

    public override bool Equals(
        [NotNullWhen(true)] object? obj)
    {
        return
            obj is Scru128 other &&
            this.Equals(other);
    }

    public override int GetHashCode()
    {
        var span = this.AsReadOnlySpan();

#if NET6_0_OR_GREATER
        var hashCode = new HashCode();
        hashCode.AddBytes(span);
        return hashCode.ToHashCode();
#else
        int code1 = HashCode.Combine(span[0], span[1], span[2], span[3], span[4], span[5], span[6], span[7]);
        int code2 = HashCode.Combine(span[8], span[9], span[10], span[11], span[12], span[13], span[14], span[15]);
        return HashCode.Combine(code1, code2);
#endif
    }

    public int CompareTo(
        Scru128 other)
    {
        return this.AsReadOnlySpan().SequenceCompareTo(other.AsReadOnlySpan());
    }

    public int CompareTo(
        object? obj)
    {
        return obj switch
        {
            null => int.MaxValue,
            Scru128 other => this.CompareTo(other),
            _ => throw new ArgumentException(Resources.ArgumentMustBeScru128, nameof(obj))
        };
    }
}
