using TinyOS.Hardware;

using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TinyOS.Devices;
using TinyOS.Hosting;
using TinyOS.Boards;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace TinyOS.Networking;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = BoardApplication.CreateBuilder();

        builder.Services.AddDiagnostics();   
        builder.Services.AddTouchScreen();

        var settings = new EthernetSettings() { SubnetMask = "2.2.2.2" };
        builder.UseEthernet("eth0", settings);

        var app = builder.Build();

        var adapters = app.Services.GetRequiredService<INetworkAdapterCollection>();
        
        //var ethernet = adapters.Primary<IWiredNetworkAdapter>();

        if (adapters.Count == 0)
        {
            Console.WriteLine("Network adapters not found");
        }
        else
        {
            adapters.NetworkConnected += AdapterConnected;
            adapters.NetworkDisconnected += AdapterDisconnected;
        }
        GetWirelessQuality();
        GetOsInfo();
        //GetCpuInfo();
        app.Run();

    }

    private static void GetWirelessQuality()
    {
        var deviceName = File.ReadAllText("/etc/hostname").Trim();

    }
    
    private static void GetOsInfo()
    {
        Dictionary<string, string> _osInfo = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        var content = File.ReadAllText("/etc/os-release");
        var lines = content.Split('\n');
        foreach (var line in lines)
        {
            var items = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 2)
            {
                _osInfo.Add(
                    items[0].Trim(),
                    items[1].Replace("\"", string.Empty).Trim());
            }
        }
    }

    private static void GetCpuInfo()
    {
        Dictionary<string, string> data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        using var reader = File.OpenText("/proc/cpuinfo");

        // var done = false;

        // while (!reader.EndOfStream)
        // {
        //     var line = reader.ReadLine();
        //     if (line != null)
        //     {
        //         var items = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        //         //data.Add(items[0], items[1]);
        //         if (items.Length == 2)
        //         {
        //          data.Add(items[0], items[1]);
        //         //     // switch (items[0].Trim())
        //         //     // {
        //         //     //     case "model name":
        //         //     //         ProcessorType = items[1].Trim();
        //         //     //         done = true;
        //         //     //         break;
        //         //     // }
        //         }
        //     }
        //     if (done) break;
        // }
    }
        // IHost board = BoardHost.CreateDefaultBuilder()
        //     .ConfigureServices((context, services) =>
        //     {
        //         services.AddDiagnostics();
        //         //services 
        //         // services.AddWireless("ssid", "password");
        //         // services.AddNetworkTime();

        //         // services.AddHostedService(typeof(NetworkStatusService));

        //     }).Build();

        // board.StartAsync();


    //     Console.WriteLine("Run...");

    //     var adaptersCollection = new NetworkAdapterCollection
    //     {
    //         new WiredNetworkAdapter(),
    //         new WiFiNetworkAdapter(),
    //     };
    //     NetworkAdapters = adaptersCollection;

    //     var ethernet = NetworkAdapters.Primary<IWiredNetworkAdapter>();

    //     if (ethernet == null)
    //     {
    //         Console.WriteLine("Wired network adapters not found");
    //     }
    //     else
    //     {
    //         ethernet.NetworkConnected += EthernetAdapterConnected;
    //         ethernet.NetworkDisconnected += EthernetAdapterDisconnected;

    //         if (ethernet.IsConnected)
    //         {
    //             DisplayNetworkInformation();

    //             while (true)
    //             {
    //                 try
    //                 {
    //                     //await GetWebPageViaHttpClient("https://postman-echo.com/get?foo1=bar1&foo2=bar2");
    //                 }
    //                 catch (Exception ex)
    //                 {
    //                     Console.WriteLine($"{ex.Message}");
    //                     Task.Delay(3000);
    //                 }
    //             }
    //         }
    //     }
    // }

    private static void AdapterConnected(INetworkAdapter networkAdapter, NetworkConnectionEventArgs e)
    {
        Console.WriteLine($"{networkAdapter.Name} - Network cable connected");
        Console.WriteLine($"ip:{e.IpAddress} mask:{e.Subnet} gw:{e.Gateway}");
    }

    private static void AdapterDisconnected(INetworkAdapter networkAdapter, NetworkDisconnectionEventArgs args)
    {
        Console.WriteLine($"{networkAdapter.Name} - {args.Reason} Network cable disconnected");
    }

    public static void DisplayNetworkInformation()
    {
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

        if (adapters.Length == 0)
        {
            Console.WriteLine("No adapters available");
        }
        else
        {
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                Console.WriteLine("");
                Console.WriteLine(adapter.Description);
                Console.WriteLine(string.Empty.PadLeft(adapter.Description.Length, '='));
                Console.WriteLine($"  Adapter name: {adapter.Name}");
                Console.WriteLine($"  Interface type .......................... : {adapter.NetworkInterfaceType}");
                Console.WriteLine($"  Physical Address ........................ : {adapter.GetPhysicalAddress()}");
                Console.WriteLine($"  Operational status ...................... : {adapter.OperationalStatus}");

                string versions = string.Empty;

                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }

                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }

                Console.WriteLine($"  IP version .............................. : {versions}");

                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
                    Console.WriteLine($"  MTU ..................................... : {ipv4.Mtu}");
                }

                if ((adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) || (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
                {
                    foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine($"  IP address .............................. : {ip.Address}");
                            Console.WriteLine($"  Subnet mask ............................. : {ip.IPv4Mask}");
                        }
                    }
                }
            }
        }
    }
}
