using LatgaAutomatasConsole.Scenarios.Interfaces;
using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services;
using LatgaAutomatasLibrary.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatgaAutomatasConsole.Scenarios
{
    internal class TryToBuyToMuchProducts : IRunScenario
    {
        private readonly IVendingMachine _vendingMachine;
        private readonly IBillService _billService;
        private readonly ILogger<TryToBuyToMuchProducts> _logger;

        public TryToBuyToMuchProducts(IVendingMachine vendingMachine,
            IBillService billService,
            ILogger<TryToBuyToMuchProducts> logger)
        {
            _vendingMachine = vendingMachine;
            _billService = billService;
            _logger = logger;
        }

        public void Run()
        {
            //define bills
            _billService.ClearBills();
            _billService.DefineBill(0.1m);
            _billService.DefineBill(0.2m);
            _billService.DefineBill(0.5m);

            //define products
            Product product1 = new() { Name = "Product1", Price = 0.1m };
            Product product2 = new() { Name = "Product2", Price = 0.2m };
            Product product3 = new() { Name = "Product3", Price = 0.3m };

            //add cash to machine
            _vendingMachine.AddBill(_billService.ExchangeToBill(0.1m), 10);
            _vendingMachine.AddBill(_billService.ExchangeToBill(0.2m), 10);
            _vendingMachine.AddBill(_billService.ExchangeToBill(0.5m), 10);

            //add products to machine
            _vendingMachine.AddProduct(product1, 1);

            //add products to customer bag
            Dictionary<Product, int> customerBag = new();
            customerBag.Add(product1, 2);

            _logger.LogInformation("Customer bag:");
            customerBag.ToList().ForEach(b => _logger.LogInformation("Product {Name}, price {Price}, amount {Amount}", b.Key.Name, b.Key.Price, b.Value));

            //define customer cash
            Dictionary<Bill, int> customerCash = new();
            customerCash.Add(_billService.ExchangeToBill(0.5m), 1);

            _logger.LogInformation("Customer cash:");
            customerCash.ToList().ForEach(b => _logger.LogInformation("Nominal {Nominal}, amount {Amount}", b.Key.Nominal, b.Value));

            //try to buy
            var rest = _vendingMachine.Sell(customerBag, customerCash);

            if (!rest.Success)
            {
                _logger.LogError("Payment was unsuccessful.");
                _logger.LogError(rest.ErrorMessage);
                return;
            }

            _logger.LogInformation("Rest:");
            rest.Rest.ToList().ForEach(r => _logger.LogInformation("Nominal {Nominal}, amount {Amount}", r.Key.Nominal, r.Value));
        }
    }
}
