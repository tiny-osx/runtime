using System.Collections.Generic;
using System.Diagnostics;

namespace TinyOS.Native;

public class Platform
{
    public static string GetHostName()
    {
        try
        {
            return File.ReadAllText("/etc/hostname").Trim();
        }
        catch
        {
            return string.Empty;
        }
    }

    public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, long>> GetWirelessInfo()
    {
        var wirelessInfo = new Dictionary<string, IReadOnlyDictionary<string, long>>(StringComparer.OrdinalIgnoreCase);

        try
        {
            var content = File.ReadAllText("/proc/net/wireless");
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("wlan", StringComparison.OrdinalIgnoreCase))
                {
                    var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length == 11)
                    {
                        wirelessInfo.Add(
                            items[0].Replace(":", string.Empty).Trim(),
                            new Dictionary<string, long>()
                            {
                                { "Status", long.Parse(items[1]) },
                                { "QualityLink", long.Parse(items[2].Trim('.')) },
                                { "QualityLevel", long.Parse(items[3].Trim('.')) },
                                { "QualityNoise", long.Parse(items[4]) },
                                { "PacketNwid", long.Parse(items[5]) },
                                { "PacketCrypt", long.Parse(items[6]) },
                                { "PacketFraq", long.Parse(items[7]) },
                                { "PacketRetry", long.Parse(items[8]) },
                                { "PacketMisc", long.Parse(items[9]) },
                                { "MissedBeacon", long.Parse(items[10]) },
                            }.AsReadOnly()
                        );
                    }
                }
            }
        }
        catch
        {
            wirelessInfo = new Dictionary<string, IReadOnlyDictionary<string, long>>(StringComparer.OrdinalIgnoreCase);
        }

        return wirelessInfo.AsReadOnly();
    }

    public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, long>> GetInterfaceInfo()
    {
        var interfaceInfo = new Dictionary<string, IReadOnlyDictionary<string, long>>(StringComparer.OrdinalIgnoreCase);

        try
        {
            var content = File.ReadAllText("/proc/net/dev");
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("lo", StringComparison.OrdinalIgnoreCase)
                    | line.Trim().StartsWith("eth", StringComparison.OrdinalIgnoreCase)
                    | line.Trim().StartsWith("wlan", StringComparison.OrdinalIgnoreCase)
                    | line.Trim().StartsWith("usb", StringComparison.OrdinalIgnoreCase))
                {
                    var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length == 17)
                    {
                        interfaceInfo.Add(
                            items[0].Replace(":", string.Empty).Trim(),
                            new Dictionary<string, long>()
                            {
                                { "RxBytes", long.Parse(items[1]) },
                                { "RxPackets", long.Parse(items[2]) },
                                { "RxErrs", long.Parse(items[3]) },
                                { "RxDrop", long.Parse(items[4]) },
                                { "RxFifo", long.Parse(items[5]) },
                                { "RxFrame", long.Parse(items[6]) },
                                { "RxCompressed", long.Parse(items[7]) },
                                { "RxMulticast", long.Parse(items[8]) },
                                { "TxBytes", long.Parse(items[9]) },
                                { "TxPackets", long.Parse(items[10]) },
                                { "TxErrs", long.Parse(items[11]) },
                                { "TxDrop", long.Parse(items[12]) },
                                { "TxFifo", long.Parse(items[13]) },
                                { "TxFrame", long.Parse(items[14]) },
                                { "TxCompressed", long.Parse(items[15]) },
                                { "TxMulticast", long.Parse(items[16]) }
                            }.AsReadOnly()
                        );
                    }
                }
            }
        }
        catch
        {
            interfaceInfo = new Dictionary<string, IReadOnlyDictionary<string, long>>(StringComparer.OrdinalIgnoreCase);
        }

        return interfaceInfo.AsReadOnly();
    }

    public static IReadOnlyDictionary<string, string> GetOsInfo()
    {
        var osInfo = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            var content = File.ReadAllText("/etc/os-release");
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var items = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (items.Length == 2)
                {
                    osInfo.Add(
                        items[0].Trim(),
                        items[1].Replace("\"", string.Empty).Trim());
                }
            }
        }
        catch
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        return osInfo.AsReadOnly();
    }

    public static IReadOnlyDictionary<int, IReadOnlyDictionary<string, string>> GetCpuInfo()
    {
        var cpuInfo = new Dictionary<int, IReadOnlyDictionary<string, string>>();

        try
        {
            var content = File.ReadAllText("/proc/cpuinfo");
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var processorID = -1;
            var processor = new Dictionary<int, Dictionary<string, string>>
            {
                { processorID, new Dictionary<string, string>() }
            };

            foreach (var line in lines)
            {
                if (line.StartsWith("processor", StringComparison.OrdinalIgnoreCase))
                {
                    processorID++;
                    processor.Add(processorID, new Dictionary<string, string>());
                }

                var items = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                if (items.Length == 2)
                {  
                    if (items[0].Trim().StartsWith("revision", StringComparison.OrdinalIgnoreCase)
                        | items[0].StartsWith("serial", StringComparison.OrdinalIgnoreCase)
                        | items[0].StartsWith("model", StringComparison.OrdinalIgnoreCase))
                    { 
                        processor[-1].Add(
                            items[0].Trim(),
                            items[1].Replace(":", string.Empty).Trim());
                    }
                    else
                    {
                        processor[processorID].Add(
                            items[0].Trim(),
                            items[1].Replace(":", string.Empty).Trim());
                    }
                }
            }

            foreach(var obj in processor)
            {
                cpuInfo.Add(obj.Key, obj.Value.AsReadOnly());
            }
        
        }
        catch
        {
            return new Dictionary<int, IReadOnlyDictionary<string, string>>();
        }

        return cpuInfo.AsReadOnly();
    }

    public static IReadOnlyDictionary<string, long> GetMemoryInfo()
    {
        var memoryInfo = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);

        try
        {
            var content = File.ReadAllText("/proc/meminfo");
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (items.Length == 3)
                {
                    memoryInfo.Add(
                        items[0].Replace(":", string.Empty).Trim(),
                        long.Parse(items[1]));
                }
            }
        }
        catch
        {
            return new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
        }

        return memoryInfo.AsReadOnly();
    }
}