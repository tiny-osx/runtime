using System;

namespace TinyOS.Hardware;

/// <summary>
/// Data relating to a WiFi disconnect event.
/// </summary>
public class NetworkDisconnectionEventArgs : EventArgs
{
    /// <summary>
    /// Date and time the event was generated.
    /// </summary>
    public DateTime When { get; private set; }

    /// <summary>
    /// Disconnect reason
    /// </summary>
    public NetworkDisconnectReason Reason { get; }

    /// <summary>
    /// Construct a NetworkDisconnectionEventArgs object.
    /// </summary>
    public NetworkDisconnectionEventArgs(NetworkDisconnectReason reason)
    {
        When = DateTime.UtcNow;
        Reason = reason;
    }
}
