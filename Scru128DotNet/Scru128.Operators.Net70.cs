#if NET7_0_OR_GREATER

using System.Numerics;

namespace Scru128DotNet;

public readonly partial struct Scru128 :
    IEqualityOperators<Scru128, Scru128, bool>,
    IComparisonOperators<Scru128, Scru128, bool>
{
}

#endif
