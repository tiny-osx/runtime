using TinyOS.Gateway.WiFi;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace TinyOS.Hardware;

/// <summary>
/// Provides an abstraction for a wifi-enabled INetworkAdapter
/// </summary>
public interface IWiFiNetworkAdapter : IWirelessNetworkAdapter
{
    /// <summary>
    /// Access point the adapter is currently connected to
    /// </summary>
    string? Ssid { get; }

    /// <summary>
    /// BSSID (MAC) of the access point the ESP32 is currently connected to.
    /// </summary>
    PhysicalAddress Bssid { get; }

    /// <summary>
    /// Automatically attempt to connect to the <b>DefaultSsid</b> network after a power cycle or reset.
    /// </summary>
    bool AutoConnect { get; }

    /// <summary>
    /// Automatically try to reconnect to an access point if there is a problem / disconnection
    /// </summary>
    bool AutoReconnect { get; }

    /// <summary>
    /// Default access point to try to connect to if the network interface is started and the board
    /// is configured to automatically reconnect.
    /// </summary>
    string DefaultSsid { get; }

    /// <summary>
    /// WiFi channel used for communication.
    /// </summary>
    int Channel { get; }

    /// <summary>
    /// Start a WiFi network.
    /// </summary>
    /// <param name="ssid">Name of the network to connect to.</param>
    /// <param name="password">Password for the network.</param>
    /// <param name="timeout">Timeout period for the connection attempt</param>
    /// <param name="token">Cancellation token for the connection attempt</param>
    /// <param name="reconnection">Should the adapter reconnect automatically?</param>
    /// <exception cref="ArgumentNullException">Thrown if the ssid is null or empty or the password is null.</exception>
    Task Connect(string ssid, string password, TimeSpan timeout, CancellationToken token, ReconnectionType reconnection = ReconnectionType.Automatic);

    /// <summary>
    /// Start a WiFi network.
    /// </summary>
    /// <param name="ssid">Name of the network to connect to.</param>
    /// <param name="password">Password for the network.</param>
    /// <param name="reconnection">Should the adapter reconnect automatically?</param>
    /// <exception cref="ArgumentNullException">Thrown if the ssid is null or empty or the password is null.</exception>
    async Task Connect(string ssid, string password, ReconnectionType reconnection = ReconnectionType.Automatic)
    {
        var src = new CancellationTokenSource();
        await Connect(ssid, password, TimeSpan.Zero, src.Token, reconnection);
    }

    /// <summary>
    /// Start a WiFi network.
    /// </summary>
    /// <param name="ssid">Name of the network to connect to.</param>
    /// <param name="password">Password for the network.</param>
    /// <param name="token">Cancellation token for the connection attempt</param>
    /// <param name="reconnection">Should the adapter reconnect automatically?</param>
    /// <exception cref="ArgumentNullException">Thrown if the ssid is null or empty or the password is null.</exception>
    async Task Connect(string ssid, string password, CancellationToken token, ReconnectionType reconnection = ReconnectionType.Automatic)
    {
        await Connect(ssid, password, TimeSpan.Zero, token, reconnection);
    }

    /// <summary>
    /// Start a WiFi network.
    /// </summary>
    /// <param name="ssid">Name of the network to connect to.</param>
    /// <param name="password">Password for the network.</param>
    /// <param name="timeout">Timeout period for the connection attempt</param>
    /// <param name="reconnection">Should the adapter reconnect automatically?</param>
    /// <exception cref="ArgumentNullException">Thrown if the ssid is null or empty or the password is null.</exception>
    async Task Connect(string ssid, string password, TimeSpan timeout, ReconnectionType reconnection = ReconnectionType.Automatic)
    {
        var src = new CancellationTokenSource();
        await Connect(ssid, password, timeout, src.Token, reconnection);
    }

    /// <summary>
    /// Start a WiFi network.
    /// </summary>
    /// <param name="ssid">Name of the network to connect to.</param>
    /// <param name="password">Password for the network.</param>
    /// <param name="timeout">Timeout period for the connection attempt</param>
    /// <param name="token">Cancellation token for the connection attempt</param>
    async Task Connect(string ssid, string password, TimeSpan timeout, CancellationToken token)
    {
        await Connect(ssid, password, timeout, token, ReconnectionType.Automatic);
    }

    /// <summary>
    /// Start a WiFi network.
    /// </summary>
    /// <param name="ssid">Name of the network to connect to.</param>
    /// <param name="password">Password for the network.</param>
    /// <param name="timeout">Timeout period for the connection attempt</param>
    async Task Connect(string ssid, string password, TimeSpan timeout)
    {
        await Connect(ssid, password, timeout, CancellationToken.None);
    }

    /// <summary>
    /// Start a WiFi network.
    /// </summary>
    /// <param name="ssid">Name of the network to connect to.</param>
    /// <param name="password">Password for the network.</param>
    /// <returns>true if the connection was successfully made.</returns>
    async Task Connect(string ssid, string password)
    {
        await Connect(ssid, password, TimeSpan.FromSeconds(90), CancellationToken.None);
    }

    /// <summary>
    /// Disconnect from the currently active access point.
    /// </summary>
    /// <remarks>
    /// Setting turnOffWiFiInterface to true will call StopWiFiInterface following
    /// the disconnection from the current access point.
    /// </remarks>
    /// <param name="turnOffWiFiInterface">Should the WiFi interface be turned off?</param>
    Task Disconnect(bool turnOffWiFiInterface);

    /// <summary>
    /// Connect to the default access point.
    /// </summary>
    /// <remarks>The access point credentials should be stored in the coprocessor memory.</remarks>
    public Task ConnectToDefaultAccessPoint()
    {
        return ConnectToDefaultAccessPoint(TimeSpan.FromSeconds(90), CancellationToken.None);
    }

    /// <summary>
    /// Connect to the default access point.
    /// </summary>
    /// <remarks>The access point credentials should be stored in the coprocessor memory.</remarks>
    Task ConnectToDefaultAccessPoint(TimeSpan timeout, CancellationToken token);

    /// <summary>
    /// Removed any stored access point information from the coprocessor memory.
    /// </summary>
    Task ClearStoredAccessPointInformation();

    /// <summary>
    /// Get the list of access points.
    /// </summary>
    /// <remarks>
    /// The network must be started before this method can be called.
    /// </remarks>
    /// <returns>An `IList` (possibly empty) of access points.</returns>
    public Task<IList<WifiNetwork>> Scan()
    {
        return Scan(TimeSpan.FromMilliseconds(-1));
    }

    /// <summary>
    /// Get the list of access points.
    /// </summary>
    /// <remarks>
    /// The network must be started before this method can be called.
    /// </remarks>
    /// <param name="token">A <see cref="CancellationToken"/> to be used if the Scan should be canceled.</param>
    /// <returns>An `IList` (possibly empty) of access points.</returns>
    Task<IList<WifiNetwork>> Scan(CancellationToken token);
    /// <summary>
    /// Get the list of access points.
    /// </summary>
    /// <remarks>
    /// The network must be started before this method can be called.
    /// </remarks>
    /// <param name="timeout">A <see cref="TimeSpan"/> representing the time to search before giving up.</param>
    /// <returns>An `IList` (possibly empty) of access points.</returns>
    Task<IList<WifiNetwork>> Scan(TimeSpan timeout);
}
