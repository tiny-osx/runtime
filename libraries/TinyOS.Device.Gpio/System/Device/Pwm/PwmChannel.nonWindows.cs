﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.CompilerServices;

namespace System.Device.Pwm
{
    /// <summary>
    /// Represents a single PWM channel.
    /// </summary>
    public abstract partial class PwmChannel
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static PwmChannel CreateWindows10PwmChannel(int chip, int channel, int frequency, double dutyCyclePercentage)
        {
            // If we land in this method it means the console application is running on Windows and targetting net6.0 (without specifying Windows platform)
            // In order to call WinRT code in net6.0 it is required for the application to target the specific platform
            // so we throw the bellow exception with a detailed message in order to instruct the consumer on how to move forward.
            throw new PlatformNotSupportedException(CommonHelpers.GetFormattedWindowsPlatformTargetingErrorMessage(nameof(PwmChannel)));
        }
    }
}
