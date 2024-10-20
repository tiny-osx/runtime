using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TinyOS.Hardware;
using TinyOS.Devices;
using System.Net.NetworkInformation;
using System.ComponentModel;

namespace TinyOS.Boards
{
    public static class NetworkingServiceCollectionExtension
    {
        public static IServiceCollection AddNetworking(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            var adaptersCollection = new NativeNetworkAdapterCollection();
            services.TryAddSingleton<INetworkAdapterCollection>(adaptersCollection);

            services.AddHostedService<NetworkingService>();

            return services;
        }

        public static IServiceCollection AddEthernet(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            return services;
        }

        public static IServiceCollection AddWireless(
         this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            return services;
        }

        public static IServiceCollection AddGadget(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            return services;
        }

        public static IHostApplicationBuilder UseEthernet(
            this IHostApplicationBuilder builder, 
            string name,
            EthernetSettings settings)
        {
            if (builder == null)
            {
                throw new ArgumentNullException();
            }
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            if (settings == null)
            {
                throw new ArgumentNullException();
            }
            
            builder.Configuration[$"{name}:Enabled"] = "True";

            if (!string.IsNullOrWhiteSpace(settings.IpAddress))
            {
                builder.Configuration[$"{name}:IpAddress"] = settings.IpAddress;
            }

            if (!string.IsNullOrWhiteSpace(settings.SubnetMask))
            {
                builder.Configuration[$"{name}:SubnetMask"] = settings.SubnetMask;
            }

            if (!string.IsNullOrWhiteSpace(settings.Gateway))
            {
                builder.Configuration[$"{name}:Gateway"] = settings.Gateway;
            }

            if (!string.IsNullOrWhiteSpace(settings.DnsAddresses))
            {
                builder.Configuration[$"{name}:SubnetMask"] = settings.DnsAddresses;
            }

            return builder;
        }
    }

    public class EthernetSettings
    {
        public string IpAddress { get; set; } = string.Empty;
        public string DnsAddresses { get; set; } = string.Empty;
        public string SubnetMask { get; set; } = string.Empty;
        public string Gateway { get; set; } = string.Empty;
    }

    public class GadgetSettings
    {
        public bool Enabled { get; set; }
    }

    public class WirelessSettings
    {
        public bool AutoConnect { get; set; }
        public bool AutoReconnect { get; set; }
        public string? Ssid { get; set; }
        public string? Psk { get; set; }
        public string? IpAddress { get; set; }
        public string? DnsAddresses { get; set; }
        public string? SubnetMask { get; set; }
        public string? Gateway { get; set; }
    }
}



