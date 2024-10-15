// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;
//using LibgpiodV2 = Interop.Libgpiod;

namespace System.Device.Gpio.Libgpiod;

internal class ChipSafeHandle : SafeHandle
{
    public ChipSafeHandle()
        : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        Interop.Libgpiod.gpiod_chip_close(handle);
        return true;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;
}
