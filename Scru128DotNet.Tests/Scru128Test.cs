﻿using System;
using System.Runtime.CompilerServices;

namespace Scru128DotNet.Tests;

public class Scru128Test
{
    [Fact]
    public void 構造体のサイズは16バイト()
    {
        int actual = Unsafe.SizeOf<Scru128>();

        Assert.Equal(16, actual);
    }

    [Fact]
    public void Test1()
    {
        var data = new Scru128(
            0x1234_5678_9abc,
            0xde_f013,
            0x57_9bdf,
            0x2468_ace0);

        Span<byte> buffer = stackalloc byte[16];

        bool ok = data.TryWriteBytes(buffer);
        Assert.True(ok);

        ReadOnlySpan<byte> expected = new byte[]
        {
            0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc,
            0xde, 0xf0, 0x13,
            0x57, 0x9b, 0xdf,
            0x24, 0x68, 0xac, 0xe0
        }.AsSpan();

        Assert.True(buffer.SequenceEqual(expected));
    }
}
