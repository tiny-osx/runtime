using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TinyOS.GPIO
{
    internal class LedService(HardwareService hardware) : BackgroundService
    {
        private readonly HardwareService _hardware = hardware;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var ledPin = 24;
            bool ledOn = true;

            GpioPin led = _hardware.GpioController.OpenPin(ledPin, PinMode.Output);
            led.Write(PinValue.Low);

            while (!stoppingToken.IsCancellationRequested)
            {
                led.Write(ledOn ? PinValue.High : PinValue.Low);
                await Task.Delay(1000, stoppingToken);
                ledOn = !ledOn;
            }
        }
    }
}