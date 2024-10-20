using TinyOS.Hardware;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TinyOS.Devices;

/// <summary>
/// Provides a collection of .NET native network adapters
/// </summary>
public class NativeNetworkAdapterCollection : INetworkAdapterCollection
{
    /// <inheritdoc/>
    public event NetworkConnectionHandler NetworkConnected = delegate { };
    /// <inheritdoc/>
    public event NetworkDisconnectionHandler NetworkDisconnected = delegate { };

    private readonly List<INetworkAdapter> _adapters = new List<INetworkAdapter>();

    /// <summary>
    /// Gets an INetworkAdapter from the collection at a specified index
    /// </summary>
    /// <param name="index">The index of the adapter to retrieve</param>
    public INetworkAdapter this[int index] => _adapters[index];

    /// <summary>
    /// Creates a NativeNetworkAdapterCollection
    /// </summary>
    public NativeNetworkAdapterCollection()
    {
        LoadAdapters();
    }

    /// <summary>
    /// Adds an INetworkAdapter to the collection
    /// </summary>
    /// <param name="adapter"></param>
    public void Add(INetworkAdapter adapter)
    {
        _adapters.Add(adapter);

        adapter.NetworkConnected += (s, e) => NetworkConnected?.Invoke(s, e);
        adapter.NetworkDisconnected += (s, e) => NetworkDisconnected?.Invoke(s, e);
    }

    /// <summary>
    /// Gets an enumerator for the collection
    /// </summary>
    public IEnumerator<INetworkAdapter> GetEnumerator()
    {
        return _adapters.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Refreshes the status of adapters
    /// </summary>
    public Task Refresh()
    {
        foreach (var adapter in _adapters)
        {
            adapter.Refresh();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Refreshes the collection of adapters
    /// </summary>
    public void RefreshAdapters()
    {
        lock (_adapters)
        {
            _adapters.Clear();

            LoadAdapters();
        }
    }

    private void LoadAdapters()
    {
        try
        {
            var adapters = NetworkInterface.GetAllNetworkInterfaces();

            if (adapters.Length > 0)
            {
                foreach (var adapter in adapters)
                {
                    if (adapter.Id.StartsWith("eth"))
                    {
                        Add(new WiredNetworkAdapter(adapter));
                    }
                    else if (adapter.Id.StartsWith("wlan"))
                    {
                        Add(new WiFiNetworkAdapter(adapter));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message); // Error
        }
    }
}
