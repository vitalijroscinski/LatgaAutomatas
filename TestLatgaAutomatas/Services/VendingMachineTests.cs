using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services;

namespace TestLatgaAutomatas.Services
{
    public class VendingMachineTests
    {
        [Fact]
        public void Sell_WhenCustomerPaysWith50CentFor10CentProduct_ShouldReturnTwo20CentCoinsInChange()
        {
            Bill bill_10ct = new() { Name = "10ct", Nominal = 0.1m };
            Bill bill_20ct = new() { Name = "20ct", Nominal = 0.2m };
            Bill bill_50ct = new() { Name = "50ct", Nominal = 0.5m };

            Product product1 = new() { Name = "Product1", Price = 0.1m };

            var vendingMachine = new VendingMachine(new PaymentService());

            vendingMachine.AddBill(bill_10ct, 10);
            vendingMachine.AddBill(bill_20ct, 10);
            vendingMachine.AddBill(bill_50ct, 10);

            vendingMachine.AddProduct(product1, 1);

            Dictionary<Product, int> bag = new() { { product1, 1 } };
            Dictionary<Bill, int> cash = new() { { bill_50ct, 1 } };

            var rest = vendingMachine.Sell(bag, cash);

            Assert.True(rest.Success);
            Assert.True(BillsEquals(rest.Rest, new() { { bill_20ct, 2 } }));
        }

        [Fact]
        public void Sell_WhenCustomerWantToBuyNonExistingProduct_MachineShouldReturnSpecificError()
        {
            Bill bill_10ct = new() { Name = "10ct", Nominal = 0.1m };
            Bill bill_20ct = new() { Name = "20ct", Nominal = 0.2m };
            Bill bill_50ct = new() { Name = "50ct", Nominal = 0.5m };

            Product product1 = new() { Name = "Product1", Price = 0.1m };

            var vendingMachine = new VendingMachine(new PaymentService());

            vendingMachine.AddBill(bill_10ct, 10);
            vendingMachine.AddBill(bill_20ct, 10);
            vendingMachine.AddBill(bill_50ct, 10);

            //vendingMachine.AddProduct(product1, 1);

            Dictionary<Product, int> bag = new() { { product1, 1 } };
            Dictionary<Bill, int> cash = new() { { bill_50ct, 1 } };

            var rest = vendingMachine.Sell(bag, cash);

            Assert.True(!rest.Success);
            Assert.True(rest.ErrorMessage.Contains("Some products are missing"));
        }

        private bool BillsEquals(Dictionary<Bill, int> bills1, Dictionary<Bill, int> bills2)
        {
            if (bills1 == null || bills2 == null)
                return false;

            return bills1.All(x => bills2.ContainsKey(x.Key) && bills1[x.Key] == bills2[x.Key])
                && bills2.All(x => bills1.ContainsKey(x.Key) && bills1[x.Key] == bills2[x.Key]);
        }
    }
}
