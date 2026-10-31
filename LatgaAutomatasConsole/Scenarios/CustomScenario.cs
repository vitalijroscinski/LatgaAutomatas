using LatgaAutomatasConsole.Scenarios.Interfaces;
using LatgaAutomatasLibrary.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatgaAutomatasConsole.Scenarios
{
    internal class CustomScenario : IRunScenario
    {
        private readonly IVendingMachine _vendingMachine;
        private readonly IBillService _billService;
        private readonly ILogger<CustomScenario> _logger;

        public CustomScenario(IVendingMachine vendingMachine,
            IBillService billService,
            ILogger<CustomScenario> logger)
        {
            _vendingMachine = vendingMachine;
            _billService = billService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogError("Empty scenario was choosen. Example of usage find in any implemented IRunScenario type.");
        }
    }
}
