namespace Scru128;

public readonly partial struct Scru128
{
    public static bool operator ==(
        in Scru128 left,
        in Scru128 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(
        in Scru128 left,
        in Scru128 right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(
        in Scru128 left,
        in Scru128 right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(
        in Scru128 left,
        in Scru128 right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(
        in Scru128 left,
        in Scru128 right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(
        in Scru128 left,
        in Scru128 right)
    {
        return left.CompareTo(right) >= 0;
    }
}
