using LatgaAutomatasConsole.Scenarios;
using LatgaAutomatasConsole.Scenarios.Interfaces;
using LatgaAutomatasLibrary.Services;
using LatgaAutomatasLibrary.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LatgaAutomatasConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Configure Host
            var builder = Host.CreateApplicationBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Register services
            builder.Services.AddSingleton<IVendingMachine, VendingMachine>();
            builder.Services.AddSingleton<IPaymentService, PaymentService>();
            builder.Services.AddSingleton<IBillService, BillService>();

            //run scenarios
            //builder.Services.AddSingleton<IRunScenario, PayAndGetRest>();
            //builder.Services.AddSingleton<IRunScenario, TryToBuyNonExistingProduct>();
            //builder.Services.AddSingleton<IRunScenario, TryToBuyToMuchProducts>();
            builder.Services.AddSingleton<IRunScenario, CustomScenario>();

            // Build Host
            using var host = builder.Build();

            // Start Host
            await host.StartAsync();

            try
            {
                // Resolve main application service
                var runScenario = host.Services
                    .GetRequiredService<IRunScenario>();

                runScenario.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error. " + ex.Message);
            }
            finally
            {
                await host.StopAsync();
            }
        }
    }
}
