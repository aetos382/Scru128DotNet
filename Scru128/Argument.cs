using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Scru128.Properties;

namespace Scru128;

internal static class Argument
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNull<T>(
        [NotNull] T? value,
        [CallerArgumentExpression("value")] string parameterName = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidateTimestamp(
        long timestamp,
        [CallerArgumentExpression("timestamp")] string parameterName = "")
    {
        if (timestamp < 0 || timestamp > Scru128.MaxTimestamp)
        {
            throw new ArgumentOutOfRangeException(parameterName, Resources.ArgumentTimestampMustBe48BitPositiveInteger);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValidateCounter(
        long counter,
        [CallerArgumentExpression("counter")] string parameterName = "")
    {
        if (counter < 0 || counter > Scru128.MaxCounter)
        {
            throw new ArgumentOutOfRangeException(parameterName, Resources.ArgumentCounterMustBe24BitPositiveInteger);
        }
    }

    public static void ValidateByteArray(
        [NotNull] byte[] array,
        [CallerArgumentExpression("array")] string parameterName = "")
    {
        NotNull(array, parameterName);

        if (array.Length != Scru128.BytesCount)
        {
            throw new ArgumentException(Resources.ArrayConstructor, parameterName);
        }
    }

    public static void ValidateByteSpan(
        [NotNull] ReadOnlySpan<byte> span,
        [CallerArgumentExpression("span")] string parameterName = "")
    {
        if (span.Length != Scru128.BytesCount)
        {
            throw new ArgumentException(Resources.ArrayConstructor, parameterName);
        }
    }
}
