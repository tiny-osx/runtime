using System;
using System.Net;

namespace TinyOS.Hardware;

/// <summary>
/// Arguments passed in a NetworkConnection event
/// </summary>
public class NetworkConnectionEventArgs : EventArgs
{
    /// <summary>
    /// IP address of the device on the network.
    /// </summary>
    public IPAddress IpAddress { get; }

    /// <summary>
    /// Subnet mask of the device.
    /// </summary>
    public IPAddress Subnet { get; }

    /// <summary>
    /// Address of the gateway.
    /// </summary>
    public IPAddress Gateway { get; }

    /// <summary>
    /// Creates a NetworkConnectionEventArgs
    /// </summary>
    /// <param name="ipAddress">The adapter's IP Address</param>
    /// <param name="subnet">The adapter's subnet mask</param>
    /// <param name="gateway">The adapter's gateway address</param>
    public NetworkConnectionEventArgs(IPAddress ipAddress, IPAddress subnet, IPAddress gateway)
    {
        IpAddress = ipAddress;
        Subnet = subnet;
        Gateway = gateway;
    }
}
