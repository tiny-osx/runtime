using TinyOS.Peripherals.Displays;

namespace TinyOS.Hardware;

/// <summary>
/// An event delegate for touchscreen events
/// </summary>
public delegate void TouchEventHandler(ITouchScreen sender, TouchPoint point);

/// <summary>
/// Represents a touch screen device
/// </summary>
public interface ITouchScreen
{
    /// <summary>
    /// Event raised when the touchscreen is pressed
    /// </summary>
    public event TouchEventHandler TouchDown;
    /// <summary>
    /// Event raised when the touchscreen is released
    /// </summary>
    public event TouchEventHandler TouchUp;
    /// <summary>
    /// Event raised when a cycle of press and release has occurred
    /// </summary>
    public event TouchEventHandler TouchClick;
    /// <summary>
    /// Event raised when a cycle of press and release has occurred
    /// </summary>
    public event TouchEventHandler TouchMoved;

    /// <summary>
    /// Returns <b>true</b> if the touchscreen is currently being touched, otherwise <b>false</b>
    /// </summary>
    public bool IsTouched { get; }
    /// <summary>
    /// Gets the current rotation of the touchscreen
    /// </summary>
    public RotationType Rotation { get; }
}