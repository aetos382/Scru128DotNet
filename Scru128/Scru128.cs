using System;

namespace Scru128;

[Serializable]
public readonly partial struct Scru128 :
    IEquatable<Scru128>,
    IComparable<Scru128>,
    IComparable,
    IFormattable
{
    public Scru128(
        byte[] bytes)
    {
        throw new NotImplementedException();
    }

    public bool Equals(
        Scru128 other)
    {
        return this.CompareTo(other) == 0;
    }

    public override bool Equals(
        object? obj)
    {
        return obj is Scru128 other && this.CompareTo(other) == 0;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public int CompareTo(
        Scru128 other)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }

    public string ToString(
        string? format,
        IFormatProvider? formatProvider)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(
        object? obj)
    {
        if (obj is not Scru128 other)
        {
            throw new ArgumentException();
        }

        return this.CompareTo(other);
    }

    public byte[] ToByteArrray()
    {
        throw new NotImplementedException();
    }

    public static Scru128 Parse(
        string input)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(
        string input,
        out Scru128 result)
    {
        throw new NotImplementedException();
    }
}
