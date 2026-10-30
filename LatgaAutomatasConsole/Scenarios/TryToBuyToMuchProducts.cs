using LatgaAutomatasConsole.Scenarios.Interfaces;
using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services;
using LatgaAutomatasLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LatgaAutomatasConsole.Scenarios
{
    internal class TryToBuyToMuchProducts : IRunScenario
    {
        private readonly IVendingMachine _vendingMachine;
        private readonly IBillService _billService;

        public TryToBuyToMuchProducts(IVendingMachine vendingMachine,
            IBillService billService)
        {
            _vendingMachine = vendingMachine;
            _billService = billService;
        }
        public void Run()
        {
            //define bills
            _billService.DefineBill("10ct", 0.1m);
            _billService.DefineBill("20ct", 0.2m);
            _billService.DefineBill("50ct", 0.5m);

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

            Console.WriteLine("Customer bag:");
            customerBag.ToList().ForEach(b => Console.WriteLine($"Product {b.Key.Name}, price {b.Key.Price}, amount {b.Value}"));

            //define customer cash
            Dictionary<Bill, int> customerCash = new();
            customerCash.Add(_billService.ExchangeToBill(0.5m), 1);

            Console.WriteLine("Customer cash:");
            customerCash.ToList().ForEach(b => Console.WriteLine($"Name {b.Key.Name}, nominal {b.Key.Nominal}, amount {b.Value}"));

            //try to buy
            var rest = _vendingMachine.Sell(customerBag, customerCash);

            if (!rest.Success)
            {
                Console.WriteLine("Payment was unsuccessful.");
                Console.WriteLine(rest.ErrorMessage);
                return;
            }

            Console.WriteLine("Rest:");
            rest.Rest.ToList().ForEach(r => Console.WriteLine($"Name {r.Key.Name}, nominal {r.Key.Nominal}, amount {r.Value}"));
        }
    }
}
