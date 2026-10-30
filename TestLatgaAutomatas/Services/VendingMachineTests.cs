using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services;

namespace TestLatgaAutomatas.Services
{
    public class VendingMachineTests
    {
        private Bill _10ct;
        private Bill _20ct;
        private Bill _50ct;
        private Product _product1;
        private VendingMachine _vendingMachine;


        public VendingMachineTests()
        {
            _10ct = new() { Name = "10ct", Nominal = 0.1m };
            _20ct = new() { Name = "20ct", Nominal = 0.2m };
            _50ct = new() { Name = "50ct", Nominal = 0.5m };
            _product1 = new() { Name = "Product1", Price = 0.1m };

            _vendingMachine = new VendingMachine(new PaymentService());

            _vendingMachine.AddBill(_10ct, 10);
            _vendingMachine.AddBill(_20ct, 10);
            _vendingMachine.AddBill(_50ct, 10);

            _vendingMachine.AddProduct(_product1, 1);
        }

        [Fact]
        public void Sell_WhenCustomerPaysWith50CentFor10CentProduct_ShouldReturnTwo20CentCoinsInChange()
        {
            Dictionary<Product, int> bag = new() { { _product1, 1 } };
            Dictionary<Bill, int> cash = new() { { _50ct, 1 } };

            var rest = _vendingMachine.Sell(bag, cash);

            Assert.True(rest.Success);
            Assert.True(BillsEquals(rest.Rest, new() { { _20ct, 2 } }));
        }

        [Fact]
        public void Sell_WhenCustomerWantToBuyNonExistingProduct_MachineShouldReturnSpecificError()
        {
            Product nonExistingProduct = new Product() { Name = "Non existing product", Price = 0.1m };
            Dictionary<Product, int> bag = new() { { nonExistingProduct, 1 } };
            Dictionary<Bill, int> cash = new() { { _50ct, 1 } };

            var rest = _vendingMachine.Sell(bag, cash);

            Assert.True(!rest.Success);
            Assert.True(rest.ErrorMessage.Contains("Some products are missing"));
        }

        [Fact]
        public void Sell_WhenCustomerOrdersMoreProductsThanMachineContains_ShouldReturnSpecificError()
        {
            Dictionary<Product, int> bag = new() { { _product1, 2 } };
            Dictionary<Bill, int> cash = new() { { _50ct, 1 } };

            var rest = _vendingMachine.Sell(bag, cash);

            Assert.False(rest.Success);
            Assert.Contains("Not sufficent amount of this products", rest.ErrorMessage);
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
