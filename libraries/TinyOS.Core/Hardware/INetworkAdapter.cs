using System.Net;
using System.Net.NetworkInformation;

namespace TinyOS.Hardware;

/// <summary>
/// Delegate containing information about a change in network state event
/// </summary>
/// <param name="sender"></param>
public delegate void NetworkStateHandler(INetworkAdapter sender);

/// <summary>
/// Delegate containing information about a network connection event
/// </summary>
/// <param name="sender"></param>
/// <param name="args"></param>
public delegate void NetworkConnectionHandler(INetworkAdapter sender, NetworkConnectionEventArgs args);

/// <summary>
/// Delegate containing information about a network disconnection event
/// </summary>
/// <param name="sender"></param>
/// <param name="args"></param>
public delegate void NetworkDisconnectionHandler(INetworkAdapter sender, NetworkDisconnectionEventArgs args);

/// <summary>
/// Delegate containing information about a network error event.
/// </summary>
/// <param name="sender">Object sending this error</param>
/// <param name="args">Error codes and information about the error.</param>
public delegate void NetworkErrorHandler(INetworkAdapter sender, NetworkErrorEventArgs args);

/// <summary>
/// Base interface for a network adapter
/// </summary>
public interface INetworkAdapter
{
    /// <summary>
    /// Event raised when a network is connecting
    /// </summary>
    event NetworkStateHandler NetworkConnecting;

    /// <summary>
    /// Event raised when a network is connected
    /// </summary>
    event NetworkConnectionHandler NetworkConnected;

    /// <summary>
    /// Event raised when a network is disconnected
    /// </summary>
    event NetworkDisconnectionHandler NetworkDisconnected;

    /// <summary>
    /// Event raised when a auto-reconnecting to a network has terminaled
    /// </summary>
    event NetworkStateHandler NetworkConnectFailed;

    /// <summary>
    /// Event raised on an unexpected network error.
    /// </summary>
    event NetworkErrorHandler NetworkError;

    /// <summary>
    /// Gets the friendly name of the Adapter
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Indicates if the network adapter is connected to an access point.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// IP Address of the network adapter.
    /// </summary>
    IPAddress IpAddress { get; }

    /// <summary>
    /// Subnet mask of the adapter.
    /// </summary>
    IPAddress SubnetMask { get; }

    /// <summary>
    /// Default gateway for the adapter.
    /// </summary>
    IPAddress Gateway { get; }

    /// <summary>
    /// Physical (MAC) address of the adapter
    /// </summary>
    PhysicalAddress MacAddress { get; }

    /// <summary>
    /// DNS Addresses of the network adapter.
    /// </summary>
    IPAddressCollection DnsAddresses { get; }
}
