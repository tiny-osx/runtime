using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TinyOS.Hardware;

public class NetworkingService : BackgroundService
{
    private readonly ILogger<NetworkingService> _logger;
     private readonly INetworkAdapterCollection _adapters;

    public NetworkingService(
        INetworkAdapterCollection adapters,
        IConfiguration configuration, 
        ILogger<NetworkingService> logger)
    {
        _adapters = adapters;
        _logger = logger;
    }

    private readonly TimeSpan _period = TimeSpan.FromSeconds(1);

    private int _executionCount = 0;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                _adapters?.Refresh();
                _logger.LogInformation(
                    $"Executed PeriodicHostedService - Count: {_executionCount}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(
                    $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
            }
        }
    }
}