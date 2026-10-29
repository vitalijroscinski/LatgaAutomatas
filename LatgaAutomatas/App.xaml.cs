using LatgaAutomatas.Services;
using LatgaAutomatas.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace LatgaAutomatas
{
    public partial class App : Application
    {
        private IHost? _host;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();

            builder.Services.AddSingleton<IVendingMachine, VendingMachine>();
            builder.Services.AddSingleton<IPaymentService, PaymentService>();
            builder.Services.AddSingleton<MainWindow>();

            _host = builder.Build();
            await _host.StartAsync();

            var mainWindow =
                _host.Services.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }
    }
}
