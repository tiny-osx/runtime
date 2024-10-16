using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyOS.Hardware;

/// <summary>
/// Provides an abstraction for a collection of INetworkAdapters
/// </summary>
public interface INetworkAdapterCollection : IEnumerable<INetworkAdapter>
{
    /// <summary>
    /// Event raised when a network is connected on any adapter
    /// </summary>
    event NetworkConnectionHandler NetworkConnected;
    /// <summary>
    /// Event raised when a network is disconnected on any adapter
    /// </summary>
    event NetworkDisconnectionHandler NetworkDisconnected;

    /// <summary>
    /// Gets the number of network adapters in the collection
    /// </summary>
    public int Count => this.Count();

    /// <summary>
    /// Retrieves an INEtworkAdapter from the collection by index
    /// </summary>
    /// <param name="index">The index of the item in the collection</param>
    INetworkAdapter this[int index] { get; }

    /// <summary>
    /// Retrieves the first registered INetworkAdapter matching the requested type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T? Primary<T>() where T : INetworkAdapter
    {
        return this.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Refreshes the collection of Adapters
    /// </summary>
    /// <returns></returns>
    Task Refresh();
}
