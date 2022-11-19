﻿global using static Scru128.Tests.Constants;

namespace Scru128.Tests;

internal static class Constants
{
    public const long MAX_INT48 = 0x0000_ffff_ffff_ffff;
    public const int MAX_INT32 = unchecked((int)0xffff_ffff);
    public const int MAX_INT24 = 0x00ff_ffff;
}
