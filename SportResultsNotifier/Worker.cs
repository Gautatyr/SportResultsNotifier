using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SportResultsNotifier;

public sealed class WindowsBackgroundService : BackgroundService
{
    private readonly Scrapper _scrapper;
    private readonly ILogger<WindowsBackgroundService> _logger;

    public WindowsBackgroundService(
        Scrapper scrapper,
        ILogger<WindowsBackgroundService> logger) =>
        (_scrapper, _logger) = (scrapper, logger);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _scrapper.ScrapNSendGames();

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
        catch (TaskCanceledException)
        {
            // When the stopping token is canceled, for example, a call made from services.msc,
            // we shouldn't exit with a non-zero exit code. In other words, this is expected...
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            // Terminates this process and returns an exit code to the operating system.
            // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            // performs one of two scenarios:
            // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            // 2. When set to "StopHost": will cleanly stop the host, and log errors.
            //
            // In order for the Windows Service Management system to leverage configured
            // recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1);
        }
    }
}