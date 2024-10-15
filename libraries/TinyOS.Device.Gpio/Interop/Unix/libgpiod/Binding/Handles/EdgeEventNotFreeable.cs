﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Device.Gpio.Libgpiod;

/// <summary>
/// Used in case Libgpiod forbids the release
/// </summary>
internal class EdgeEventNotFreeable : EdgeEventSafeHandle
{
    public EdgeEventNotFreeable()
        : base(false)
    {
    }

    protected override bool ReleaseHandle()
    {
        return true;
    }
}
