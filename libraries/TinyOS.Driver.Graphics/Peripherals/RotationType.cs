﻿namespace TinyOS.Peripherals.Displays;

/// <summary>
/// Display rotation 
/// </summary>
public enum RotationType : int
{
    /// <summary>
    /// Default or normal orientation
    /// </summary>
    Default = 0,
    /// <summary>
    /// Default or normal orientation
    /// </summary>
    Normal = Default,
    /// <summary>
    /// Rotated 90 degrees clockwise
    /// </summary>
    _90Degrees = 90,
    /// <summary>
    /// Rotated 180 degrees clockwise
    /// </summary>
    _180Degrees = 180,
    /// <summary>
    /// Rotated 270 degrees clockwise
    /// </summary>
    _270Degrees = 270
}