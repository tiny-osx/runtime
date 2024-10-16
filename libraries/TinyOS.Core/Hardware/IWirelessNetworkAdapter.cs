namespace TinyOS.Hardware;

/// <summary>
/// Provides an abstraction for a wireless INetworkAdapter
/// </summary>
public interface IWirelessNetworkAdapter : INetworkAdapter
{
    /// <summary>
    /// Gets the current antenna in use
    /// </summary>
    AntennaType CurrentAntenna { get; }

    /// <summary>
    /// Sets the currently used antenna
    /// </summary>
    /// <param name="antenna">The antenna to use</param>
    /// <param name="persist">If this selection should persist across device resets</param>
    void SetAntenna(AntennaType antenna, bool persist = true);
}
