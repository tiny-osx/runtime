// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;
//using Libgpiod = Interop.Libgpiod;

namespace System.Device.Gpio.Libgpiod;

internal class EdgeEventSafeHandle : SafeHandle
{
    public EdgeEventSafeHandle()
        : this(true)
    {
    }

    protected EdgeEventSafeHandle(bool ownsHandle)
        : base(IntPtr.Zero, ownsHandle)
    {
    }

    protected override bool ReleaseHandle()
    {
        Interop.Libgpiod.gpiod_edge_event_free(handle);
        return true;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;
}
