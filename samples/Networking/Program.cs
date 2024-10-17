using TinyOS.Hardware;

using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TinyOS.Devices;
using TinyOS.Hosting;
using TinyOS.Boards;
using Microsoft.Extensions.Hosting;

namespace TinyOS.Networking;

public class Program
{
     public static INetworkAdapterCollection NetworkAdapters = new NetworkAdapterCollection();

    public static void Main(string[] args)
    {
        var builder = BoardApplication.CreateBuilder();
        
        builder.Services.AddDiagnostics();
        builder.Services.AddEthernet();
        builder.Services.AddTouchScreen();
        
        var deamon = builder.Build();
        deamon.Run();



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


        Console.WriteLine("Run...");

        var adaptersCollection = new NetworkAdapterCollection
        {
            new WiredNetworkAdapter(),
            new WiFiNetworkAdapter(),
        };
        NetworkAdapters = adaptersCollection;

        var ethernet = NetworkAdapters.Primary<IWiredNetworkAdapter>();

        if (ethernet == null)
        {
            Console.WriteLine("Wired network adapters not found");
        }
        else
        {
            ethernet.NetworkConnected += EthernetAdapterConnected;
            ethernet.NetworkDisconnected += EthernetAdapterDisconnected;

            if (ethernet.IsConnected)
            {
                DisplayNetworkInformation();

                while (true)
                {
                    try
                    {
                        //await GetWebPageViaHttpClient("https://postman-echo.com/get?foo1=bar1&foo2=bar2");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        Task.Delay(3000);
                    }
                }
            }
        }
    }

    private static void EthernetAdapterDisconnected(INetworkAdapter sender, NetworkDisconnectionEventArgs args)
    {
        Console.WriteLine("Network cable disconnected");
    }

    private static void EthernetAdapterConnected(INetworkAdapter networkAdapter, NetworkConnectionEventArgs e)
    {
        Console.WriteLine("Network cable connected");
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
