using System;
using System.Net;

using Microsoft.Extensions.Configuration;

internal static class ConfigurationExtensions
{
    internal static IPAddress GetIpAddress(this IConfiguration configuration, string key)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException();
        }

        if (configuration[key] is not string value)
        {
            throw new ArgumentException();
        }

        try 
        {
            return IPAddress.Parse(value);
        }
        catch
        {
            return IPAddress.None;
        }
    }

    internal static IPAddress[] GetDnsAddresses(this IConfiguration configuration, string key)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException();
        }

        if (configuration[key] is not string value)
        {
            throw new ArgumentException();
        }

        string[] array = value.Split(',');
        IPAddress[] addresses = new IPAddress[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            try
            {
                addresses[i] = IPAddress.Parse(array[i].Trim());
            }
            catch
            {
                addresses[i] = IPAddress.None;
            }
        }

        return addresses;
    }

    internal static byte[] GetMacAddress(this IConfiguration configuration, string key)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException();
        }

        if (configuration[key] is not string value)
        {
            throw new ArgumentException();
        }

        string[] array = value.Split('-');
        byte[] bytes = new byte[6];

        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte)Convert.ToInt32(array[i].Trim(), 16);
        }

        return bytes;
    }
}