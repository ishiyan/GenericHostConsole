using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenericHostConsole
{
    internal class Program
    {
        private static async Task Main(string[] args) => 
            await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    // Commented out because default builder does the same.
                    // configHost.SetBasePath(Directory.GetCurrentDirectory());
                    // configHost.AddJsonFile("appsettings.json", optional: true);
                    // configHost.AddCommandLine(args);
                })
                .ConfigureLogging((hostingContext, logging) => {
                    // Here we want only to have a console logger.
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .UseConsoleLifetime();
    }
}
