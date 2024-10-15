using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.I2c;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TinyOS.GPIO
{
    public class Program
    {
        public static void Main()
        {
            
            // var boardInfo = RaspberryBoardInfo.LoadBoardInfo();
            // cat /proc/device-tree/model
            
            List<int> validAddress = new List<int>();
            Console.WriteLine("Hello I2C!");
            // First 8 I2C addresses are reserved, last one is 0x7F
            for (int i = 8; i < 0x80; i++)
            {
                try
                {
                    I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, i));
                    var read = i2c.ReadByte();
                    validAddress.Add(i);
                }
                catch (IOException)
                {
                    // Do nothing, there is just no device
                }
            }

            Console.WriteLine($"Found {validAddress.Count} device(s) ");

            foreach (var valid in validAddress)
            {
                Console.WriteLine($"Address: 0x{valid:X}");
            }
            
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Services.AddSingleton<HardwareService>();
            builder.Services.AddHostedService<LedService>();

            IHost host = builder.Build();

            Console.WriteLine("Blinking...");
            host.Run();
        }
    }
}