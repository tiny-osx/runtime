using TinyOS.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace TinyOS.Boards
{
    public static class DiagnosticsServiceCollectionExtension
    {
        public static IServiceCollection AddDiagnostics(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            return services.AddHostedService<DiagnosticsService>(); ;
        }
    }

    public class DiagnosticsService : SchedulerService
    {
        private readonly ILogger _logger;

        public DiagnosticsService(ILoggerFactory loggerFactory)
            : base(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3))
        {
            _logger = loggerFactory.CreateLogger(nameof(DiagnosticsService));
        }

        protected override Task ExecuteAsync()
        {
            // var cpuUsage = DeviceInformation.GetCpuUsageStatistic();

            // var managedUsedIn = Memory.ManagedMemory.UsedBytes;
            // var managedFreeIn = Memory.ManagedMemory.FreeBytes;
            // var unmanagedUsedIn = Memory.UnmanagedMemory.UsedBytes;
            // var unmanagedFreeIn = Memory.UnmanagedMemory.FreeBytes;

            // var cpuMessage = $"CPU: {cpuUsage,5:f2}%";
            // var managedMessage = $"Managed: Used {managedUsedIn,10:N0} Free {managedFreeIn,10:N0}";
            // var unmanagedMessage = $"Unmanaged: Used {unmanagedUsedIn,10:N0} Free {unmanagedFreeIn,10:N0}";

            // if (unmanagedUsedIn == 0)
            // {
            //     _logger.Log(LogLevel.Information, $"{cpuMessage} {managedMessage}", null);
            // }
            // else
            // {
            //     _logger.Log(LogLevel.Information, $"{cpuMessage} {managedMessage} {unmanagedMessage}", null);
            // }
            return Task.CompletedTask;
        }
    }
}