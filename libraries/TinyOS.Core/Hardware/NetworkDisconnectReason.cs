namespace TinyOS.Hardware;

/// <summary>
/// Extension methods for NetworkDisconnectReason
/// </summary>
public static class NetworkDisconnectReasonExtensions
{
    /// <summary>
    /// Gets a string representation of a NetworkDisconnectReason
    /// </summary>
    /// <param name="reason"></param>
    /// <returns></returns>
    public static string ToString(this NetworkDisconnectReason reason)
    {
        return reason switch
        {
            NetworkDisconnectReason.Unspecified => "Unspecified",
            NetworkDisconnectReason.Inactivity => "Disconnected due to inactivity",
            NetworkDisconnectReason.TooManyDevicesConnected => "Too many devices already connected to the Access Point",
            NetworkDisconnectReason.ManualDisconnect => "Adapter was commanded to disconnect",
            NetworkDisconnectReason.IncorrectPasscode => "Incorrect passcode provided",
            NetworkDisconnectReason.InsufficientSignal => "Insufficient signal to connect",
            NetworkDisconnectReason.AccessPointDisconnected => "Access point dropped the connection",
            NetworkDisconnectReason.AccessPointNotFound => "Access point not found",
            NetworkDisconnectReason.CableDisconnected => "Cable disconnected",
            _ => $"Undefined Reason ({reason})",
        };
    }
}

/// <summary>
/// Reasons for network interface disconnection
/// </summary>
public enum NetworkDisconnectReason
{
    /// <summary>
    /// Unspecified reason for disconnection
    /// </summary>
    Unspecified = 1,

    /// <summary>
    /// Disconnection due to authenticated leave
    /// </summary>
    AuthenticatedLeave = 3,

    /// <summary>
    /// Disconnection due to inactivity
    /// </summary>
    Inactivity = 4,

    /// <summary>
    /// Disconnection because too many devices are connected
    /// </summary>
    TooManyDevicesConnected = 5,

    /// <summary>
    /// Disconnection initiated manually
    /// </summary>
    ManualDisconnect = 8,

    /// <summary>
    /// Disconnection due to incorrect passcode
    /// </summary>
    IncorrectPasscode = 15,

    /// <summary>
    /// Disconnection due to insufficient signal
    /// </summary>
    InsufficientSignal = 33,

    /// <summary>
    /// The network cable was disconnected
    /// </summary>
    CableDisconnected = 100,

    /// <summary>
    /// Disconnection because the access point was disconnected
    /// </summary>
    AccessPointDisconnected = 200,

    /// <summary>
    /// Disconnection because the access point was not found
    /// </summary>
    AccessPointNotFound = 201,
}
