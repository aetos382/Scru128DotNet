using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Scru128DotNet.Properties;

namespace Scru128DotNet;

internal static class Argument
{
    public static void NotNull<T>(
        [NotNull] T? value,
        [CallerArgumentExpression(nameof(value))] string parameterName = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    public static void ValidateTimestamp(
        long timestamp,
        [CallerArgumentExpression(nameof(timestamp))] string parameterName = "")
    {
        if (timestamp is < 0 or > Scru128.MaxTimestamp)
        {
            throw new ArgumentOutOfRangeException(parameterName, Resources.ArgumentTimestampMustBe48BitPositiveInteger);
        }
    }

    public static void ValidateCounter(
        long counter,
        [CallerArgumentExpression(nameof(counter))] string parameterName = "")
    {
        if (counter is < 0 or > Scru128.MaxCounter)
        {
            throw new ArgumentOutOfRangeException(parameterName, Resources.ArgumentCounterMustBe24BitPositiveInteger);
        }
    }

    public static void ValidateByteSpan(
        ReadOnlySpan<byte> span,
        [CallerArgumentExpression(nameof(span))] string parameterName = "")
    {
        if (span.Length != Scru128.BytesCount)
        {
            throw new ArgumentException(Resources.ArrayConstructor, parameterName);
        }
    }
}
