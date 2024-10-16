using System;

namespace TinyOS.Hardware;

/// <summary>
/// Data relating to a WiFi error event.
/// </summary>
public class NetworkErrorEventArgs : EventArgs
{
    /// <summary>
    /// Date and time the event was generated.
    /// </summary>
    public DateTime When { get; private set; }

    /// <summary>
    /// Error code.
    /// </summary>
    public uint ErrorCode { get; private set; }

    /// <summary>
    /// Construct a NetworkErrorEventArgs object.
    /// </summary>
    public NetworkErrorEventArgs(uint code)
    {
        When = DateTime.UtcNow;
        ErrorCode = code;
    }
}
