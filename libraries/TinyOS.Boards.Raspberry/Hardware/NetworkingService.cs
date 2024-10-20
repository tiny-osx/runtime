// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

// using TinyOS.Hardware;

// using System.Net.NetworkInformation;
// using System.Diagnostics;

// namespace Bytewizer.TinyCLR.Boards
// {
//     public class EthernetService
//     {
//         private readonly ILogger _logger;

//         private readonly IConfiguration _configuration;

//         private readonly INetworkAdapter? _adapter;

//         //public bool LinkConnected { get => _ethernetAdapter.IsConnected; }

//         public EthernetService(
//             INetworkAdapterCollection networkAdapters,
//             ILoggerFactory loggerFactory,
//             IConfiguration configuration)
//         {
//             _adapter = networkAdapters.Primary<IWiredNetworkAdapter>();
//             _logger = loggerFactory.CreateLogger(nameof(EthernetService));
//             _configuration = configuration;

//             if (_adapter == null)
//             {
//                 _logger.LogError("Wired network adapters not found");
//             }
//             else
//             {
//                 _adapter.NetworkConnected += EthernetAdapterConnected;
//                 _adapter.NetworkDisconnected += EthernetAdapterDisconnected;

//                 if (_adapter.IsConnected)
//                 {
//                     DisplayNetworkInformation();
//                 }
//             }
//         }

//         private void EthernetAdapterDisconnected(INetworkAdapter sender, NetworkDisconnectionEventArgs args)
//         {
//              _logger.LogInformation("Network cable disconnected");
//         }

//         private void EthernetAdapterConnected(INetworkAdapter networkAdapter, NetworkConnectionEventArgs e)
//         {
//             _logger.LogInformation("Network cable connected");
//         }

//         public static void DisplayNetworkInformation()
//         {
//             NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

//             if (adapters.Length == 0)
//             {
//                 Console.WriteLine("No adapters available");
//             }
//             else
//             {
//                 foreach (NetworkInterface adapter in adapters)
//                 {
//                     IPInterfaceProperties properties = adapter.GetIPProperties();
//                     Console.WriteLine("");
//                     Console.WriteLine(adapter.Description);
//                     Console.WriteLine(string.Empty.PadLeft(adapter.Description.Length, '='));
//                     Console.WriteLine($"  Adapter name: {adapter.Name}");
//                     Console.WriteLine($"  Interface type .......................... : {adapter.NetworkInterfaceType}");
//                     Console.WriteLine($"  Physical Address ........................ : {adapter.GetPhysicalAddress()}");
//                     Console.WriteLine($"  Operational status ...................... : {adapter.OperationalStatus}");

//                     string versions = string.Empty;

//                     if (adapter.Supports(NetworkInterfaceComponent.IPv4))
//                     {
//                         versions = "IPv4";
//                     }

//                     if (adapter.Supports(NetworkInterfaceComponent.IPv6))
//                     {
//                         if (versions.Length > 0)
//                         {
//                             versions += " ";
//                         }
//                         versions += "IPv6";
//                     }

//                     Console.WriteLine($"  IP version .............................. : {versions}");

//                     if (adapter.Supports(NetworkInterfaceComponent.IPv4))
//                     {
//                         IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
//                         Console.WriteLine($"  MTU ..................................... : {ipv4.Mtu}");
//                     }

//                     if ((adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) || (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
//                     {
//                         foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
//                         {
//                             if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
//                             {
//                                 Console.WriteLine($"  IP address .............................. : {ip.Address}");
//                                 Console.WriteLine($"  Subnet mask ............................. : {ip.IPv4Mask}");
//                             }
//                         }
//                     }
//                 }
//             }
//         }
//     }
// }