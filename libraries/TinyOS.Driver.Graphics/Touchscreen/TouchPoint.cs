using System;

namespace TinyOS.Hardware;

/// <summary>
/// Represents data for touchscreen events
/// </summary>
public readonly struct TouchPoint
    : IEquatable<TouchPoint?>, IEquatable<TouchPoint>
{
    /// <summary>
    /// Creates a TouchPoint from raw touchscreen data
    /// </summary>
    /// <param name="rawX">The raw X position data</param>
    /// <param name="rawY">The raw Y position data</param>
    /// <param name="rawZ">The raw Z position data</param>
    public static TouchPoint FromRawData(int rawX, int rawY, int? rawZ)
    {
        return new TouchPoint(0, 0, 0, rawX, rawY, rawZ);
    }

    /// <summary>
    /// Creates a TouchPoint from screen coordinates and raw screen data
    /// </summary>
    /// <param name="screenX">The X screen position</param>
    /// <param name="screenY">The Y screen position</param>
    /// <param name="screenZ">The Z screen position</param>
    /// <param name="rawX">The raw X position data</param>
    /// <param name="rawY">The raw Y position data</param>
    /// <param name="rawZ">The raw Z position data</param>
    public static TouchPoint FromScreenData(int screenX, int screenY, int screenZ, int rawX, int rawY, int? rawZ)
    {
        return new TouchPoint(screenX, screenY, screenZ, rawX, rawY, rawZ);
    }

    private TouchPoint(int screenX, int screenY, int screenZ, int rawX, int rawY, int? rawZ = null)
    {
        ScreenX = screenX;
        ScreenY = screenY;
        ScreenZ = screenZ;
        RawX = rawX;
        RawY = rawY;
        RawZ = rawZ;
    }

    /// <summary>
    /// Gets the raw X position data of the touch point
    /// </summary>
    public int RawX { get; }
    /// <summary>
    /// Gets the raw Y position data of the touch point
    /// </summary>
    public int RawY { get; }
    /// <summary>
    /// Gets the raw Z position data of the touch point
    /// </summary>
    public int? RawZ { get; }
    /// <summary>
    /// Gets the screen-coordinate X position of the touch point
    /// </summary>
    public int ScreenX { get; }
    /// <summary>
    /// Gets the screen-coordinate Y position of the touch point
    /// </summary>
    public int ScreenY { get; }
    /// <summary>
    /// Gets the screen-coordinate Z position of the touch point
    /// </summary>
    public int? ScreenZ { get; }

    /// <inheritdoc/>
    public readonly bool Equals(TouchPoint other)
    {
        if (ScreenX != other.ScreenX) return false;
        if (ScreenY != other.ScreenY) return false;
        return true;
    }

    /// <inheritdoc/>
    public readonly bool Equals(TouchPoint? other)
    {
        if (other == null) return false;
        return Equals(this, other.Value);
    }

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        return $"{RawX}, {RawY}, {RawZ}, {ScreenX}, {ScreenY}, {ScreenZ}";
    }
}