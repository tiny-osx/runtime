using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;

namespace TinyOS.GPIO
{
    internal class HardwareService : IDisposable
    {
        private readonly GpioController _gpioController;

        public HardwareService()
        {
            const int chipNumber = 0;
            _gpioController = new GpioController();
            //_gpioController = new GpioController();
        }

        public GpioController GpioController { get { return _gpioController; } }

        public void Dispose()
        {
            _gpioController.Dispose();
        }
    }
}