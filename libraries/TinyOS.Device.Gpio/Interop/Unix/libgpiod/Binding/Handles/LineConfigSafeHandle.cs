// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;
//using Libgpiod = Interop.Libgpiod;

namespace System.Device.Gpio.Libgpiod;

internal class LineConfigSafeHandle : SafeHandle
{
    public LineConfigSafeHandle()
        : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        Interop.Libgpiod.gpiod_line_config_free(handle);
        return true;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;
}
