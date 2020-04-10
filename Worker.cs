using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenericHostConsole
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly string greetingText;
        private readonly int maxIterationCount;

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime, IConfiguration configuration)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
            greetingText = configuration.GetValue<string>("Worker:greetingText");
            maxIterationCount = configuration.GetValue<int>("Worker:maxIterationCount");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"{nameof(Worker)}: starting work for {maxIterationCount} iterations.");
            Console.WriteLine("Press ESC key to simulate an exception, Ctrl+C to exit or wait till all iterations are done.");
            int count = 0;
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                while (count++ < maxIterationCount)
                {
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        throw new ApplicationException("simulated exception");
                    }

                    logger.LogInformation($"{greetingText} {count}");
                    await Task.Delay(1000, stoppingToken);
                    if (count == maxIterationCount)
                    {
                        logger.LogInformation($"Maximum count of {count} reached, work is finished.");
                    }
                }
            }
            catch (Exception ex)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    logger.LogInformation("Cancellation requested, breaking iterations.");
                }
                else
                {
                    logger.LogCritical(ex, "Unhandled exception caught, panic.");
                }
            }

            appLifetime.StopApplication();
        }
    }
}
