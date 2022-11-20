namespace Scru128DotNet;

public readonly partial struct Scru128
{
    public static bool operator ==(
        Scru128 left,
        Scru128 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(
        Scru128 left,
        Scru128 right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(
        Scru128 left,
        Scru128 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(
        Scru128 left,
        Scru128 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(
        Scru128 left,
        Scru128 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(
        Scru128 left,
        Scru128 right)
    {
        return left.CompareTo(right) >= 0;
    }
}
