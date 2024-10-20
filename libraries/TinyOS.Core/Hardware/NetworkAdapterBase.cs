using TinyOS.Hardware;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace TinyOS;

/// <summary>
/// A base class for INetworkAdapter implementations
/// </summary>
public abstract class NetworkAdapterBase : INetworkAdapter
{
    /// <inheritdoc/>
    public event NetworkStateHandler? NetworkConnecting;

    /// <inheritdoc/>
    public event NetworkConnectionHandler? NetworkConnected;

    /// <inheritdoc/>
    public event NetworkDisconnectionHandler? NetworkDisconnected;

    /// <inheritdoc/>
    public event NetworkStateHandler? NetworkConnectFailed;

    /// <summary>
    /// Raised when a network error occurs
    /// </summary>
    public event NetworkErrorHandler NetworkError = default!;

    public OperationalStatus OperationalStatus { get; private set; } = OperationalStatus.Down;
    /// <summary>
    /// returns the connection state of the NetworkAdapter
    /// </summary>
    public abstract bool IsConnected { get; }
    
    /// <summary>
    /// Gets the network interface type
    /// </summary>
    public NetworkInterfaceType InterfaceType { get; }

    /// <inheritdoc/>
    public virtual string Name => _nativeInterface?.Name ?? "<no name>";

    private readonly NetworkInterface? _nativeInterface = default!;

    /// <summary>
    /// Constructor for the NetworkAdapterBase class
    /// </summary>
    /// <param name="nativeInterface">The native interface associated with this adapter</param>
    /// <param name="type">The network type that is expected for this adapter</param>
    protected internal NetworkAdapterBase(NetworkInterface nativeInterface, NetworkInterfaceType interfaceType)
    {
        InterfaceType = interfaceType;
        _nativeInterface = nativeInterface;
    }

    /// <summary>
    /// Raises the <see cref="NetworkConnecting"/> event
    /// </summary>
    protected void RaiseNetworkConnecting()
    {
        NetworkConnecting?.Invoke(this);
    }

    /// <summary>
    /// Raises the <see cref="NetworkConnectFailed"/> event
    /// </summary>
    protected void RaiseConnectFailed()
    {
        NetworkConnectFailed?.Invoke(this);
    }

    /// <summary>
    /// Raises the <see cref="NetworkConnected"/> event
    /// </summary>
    /// <param name="args"></param>
    protected void RaiseNetworkConnected<T>(T args) where T : NetworkConnectionEventArgs
    {
        NetworkConnected?.Invoke(this, args);
    }

    /// <summary>
    /// Raises the <see cref="NetworkDisconnected"/> event
    /// </summary>
    /// <param name="args"></param>
    protected void RaiseNetworkDisconnected(NetworkDisconnectionEventArgs args)
    {
        NetworkDisconnected?.Invoke(this, args);
    }

    /// <summary>
    /// Raises the <see cref="NetworkError"/> event
    /// </summary>
    /// <param name="args"></param>
    protected void RaiseNetworkError(NetworkErrorEventArgs args)
    {
        NetworkError?.Invoke(this, args);
    }

    /// <summary>
    /// Refreshes the NetworkAdapter's information
    /// </summary>
    public void Refresh()
    {
        if (_nativeInterface?.OperationalStatus == OperationalStatus)
        {
            return;
        }
        
        switch (_nativeInterface?.OperationalStatus)
        {
            case OperationalStatus.Up:
                RaiseNetworkConnected(
                        new NetworkConnectionEventArgs(IpAddress, SubnetMask, Gateway) 
                );
            break;
                case OperationalStatus.Down:
                RaiseNetworkDisconnected(
                    new NetworkDisconnectionEventArgs(NetworkDisconnectReason.ManualDisconnect) 
                );
            break;
            case OperationalStatus.LowerLayerDown:
                RaiseNetworkDisconnected(
                    new NetworkDisconnectionEventArgs(NetworkDisconnectReason.CableDisconnected) 
                );
            break;
            case OperationalStatus.Unknown:
                RaiseNetworkDisconnected(
                    new NetworkDisconnectionEventArgs(NetworkDisconnectReason.Unspecified) 
                );
            break;
        }

        OperationalStatus = _nativeInterface?.OperationalStatus ?? OperationalStatus.Unknown;  
    }

    /// <summary>
    /// IP Address of the network adapter.
    /// </summary>
    public IPAddress IpAddress
    {
        get
        {
            if (_nativeInterface == null)
            {
                return IPAddress.None;
            }

            return _nativeInterface?.GetIPProperties()?.UnicastAddresses?.FirstOrDefault(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.Address ?? IPAddress.None;
        }
    }

    // /// <summary>
    // /// Gets the physical (MAC) address of the network adapter
    // /// </summary>
    // public PhysicalAddress MacAddress { 
    // get
    //     {
    //         if (nativeInterface == null)
    //         {
    //             return PhysicalAddress.None;
    //         }

    //         return nativeInterface?.GetIPProperties()?.UnicastAddresses?.FirstOrDefault(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.PhysicalAddress ?? PhysicalAddress.None;
    //     }
    // }

    /// <summary>
    /// DNS Addresses of the network adapter.
    /// </summary>
    public IPAddressCollection DnsAddresses
    {
        get
        {
            if (_nativeInterface == null)
            {
                throw new InvalidOperationException("Native interface is null.");
            }

            return (_nativeInterface?.GetIPProperties()?.DnsAddresses!) ?? throw new InvalidOperationException("DNS addresses could not be retrieved.");
        }
    }

    /// <summary>
    /// Subnet mask of the adapter.
    /// </summary>
    public IPAddress SubnetMask
    {
        get
        {
            if (_nativeInterface == null)
            {
                return IPAddress.None;
            }

            return _nativeInterface?.GetIPProperties()?.UnicastAddresses?.FirstOrDefault()?.IPv4Mask ?? IPAddress.None;
        }
    }

    /// <summary>
    /// Default gateway for the adapter.
    /// </summary>
    public IPAddress Gateway
    {
        get
        {
            if (_nativeInterface == null)
            {
                return IPAddress.None;
            }

            return _nativeInterface?.GetIPProperties()?.GatewayAddresses?.FirstOrDefault()?.Address ?? IPAddress.None;
        }
    }

    // private NetworkInterface? LoadAdapterInfo()
    // {
    //     try
    //     {
    //         var interfaces = NetworkInterface.GetAllNetworkInterfaces();

    //         if (interfaces.Length > 0)
    //         {
    //             foreach (var intf in interfaces)
    //             {
    //                 if (intf.NetworkInterfaceType == InterfaceType)
    //                 {
    //                     MacAddress = intf.GetPhysicalAddress();
    //                     Debug.WriteLine($"Interface: {intf.Id}: {intf.Name} {intf.NetworkInterfaceType} {intf.OperationalStatus}"); // Trace
    //                     return intf;
    //                 }
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.WriteLine(ex.Message); // Error
    //     }
    //     return null;
    // }
}
