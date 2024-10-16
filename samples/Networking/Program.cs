using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.I2c;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TinyOS.Networking
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello I2C!");
        }
    }
}