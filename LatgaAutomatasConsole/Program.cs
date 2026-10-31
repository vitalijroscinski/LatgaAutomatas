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
            builder.Services.AddScoped<IVendingMachine, VendingMachineService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IBillService, BillService>();

            //run scenarios
            //builder.Services.AddScoped<IRunScenario, PayAndGetRest>();
            //builder.Services.AddScoped<IRunScenario, TryToBuyNonExistingProduct>();
            //builder.Services.AddScoped<IRunScenario, TryToBuyToMuchProducts>();
            builder.Services.AddScoped<IRunScenario, CustomScenario>();

            // Build Host
            using var host = builder.Build();

            // Start Host
            await host.StartAsync();

            try
            {
                // Resolve main application service
                var runScenarios = host.Services
                    .GetRequiredService<IEnumerable<IRunScenario>>();

                foreach(var runScenario in runScenarios)
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
