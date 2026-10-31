using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services;

namespace TestLatgaAutomatas.Services
{
    public class VendingMachineTests
    {
        private Bill _10ct = new() { Nominal = 0.1m };
        private Bill _20ct = new() { Nominal = 0.2m };
        private Bill _50ct = new() { Nominal = 0.5m };
        private Product _10ct_product = new() { Name = "Product1", Price = 0.1m };
        private VendingMachineService _vendingMachine = new VendingMachineService(new PaymentService());


        private void PopulateDefaultData()
        {
            _vendingMachine.AddBill(_10ct, 10);
            _vendingMachine.AddBill(_20ct, 10);
            _vendingMachine.AddBill(_50ct, 10);

            _vendingMachine.AddProduct(_10ct_product, 1);
        }

        [Fact]
        public void Sell_WhenCustomerPaysWith50CentFor10CentProduct_ShouldReturnTwo20CentCoinsInChange()
        {
            PopulateDefaultData();
            Dictionary<Product, int> bag = new() { { _10ct_product, 1 } };
            Dictionary<Bill, int> cash = new() { { _50ct, 1 } };

            var rest = _vendingMachine.Sell(bag, cash);

            Assert.True(rest.Success);
            Assert.True(BillsEquals(rest.Rest, new() { { _20ct, 2 } }));
        }

        [Fact]
        public void Sell_WhenCustomerWantToBuyNonExistingProduct_MachineShouldReturnSpecificError()
        {
            PopulateDefaultData();
            Product nonExistingProduct = new Product() { Name = "Non existing product", Price = 0.1m };
            Dictionary<Product, int> bag = new() { { nonExistingProduct, 1 } };
            Dictionary<Bill, int> cash = new() { { _50ct, 1 } };

            var rest = _vendingMachine.Sell(bag, cash);

            Assert.True(!rest.Success);
            Assert.Contains("Some products are missing", rest.ErrorMessage);
        }

        [Fact]
        public void Sell_WhenCustomerExcideOrderedAmountInSequentialBuy_ShouldReturnSpecificError()
        {
            PopulateDefaultData();
            Dictionary<Product, int> bag = new() { { _10ct_product, 1 } };
            Dictionary<Bill, int> cash = new() { { _50ct, 1 } };

            var rest = _vendingMachine.Sell(bag, cash);

            Assert.True(rest.Success);

            rest = _vendingMachine.Sell(bag, cash);
            Assert.False(rest.Success);
            Assert.Contains("Not sufficent amount of this products", rest.ErrorMessage);

        }

        [Fact]
        public void Sell_WhenCustomerByeTwiceExtendingProductAmount_ShouldReturnError()
        {
            PopulateDefaultData();
            Dictionary<Product, int> bag = new() { { _10ct_product, 1 } };
            Dictionary<Bill, int> cash = new() { { _50ct, 1 } };

            var rest = _vendingMachine.Sell(bag, cash);
            Assert.True(rest.Success);

            rest = _vendingMachine.Sell(bag, cash);
            Assert.False(rest.Success);
            Assert.Contains("Not sufficent amount of this products", rest.ErrorMessage);
        }

        [Fact]
        public void Sell_WhenCustomerHasEnoughMoneyToBuyPeoductButMachineIsWithoutMoney_ShouldSellAndReturnRest()
        {
            _vendingMachine.AddProduct(_10ct_product, 1);

            Dictionary<Product, int> bag = new() { { _10ct_product, 1 } };
            Dictionary<Bill, int> cash = new() { { _10ct, 2 } };

            var rest = _vendingMachine.Sell(bag, cash);
            Assert.True(rest.Success);
            Assert.True(BillsEquals(rest.Rest, new() { { _10ct, 1 } }));
        }

        [Fact]
        public void Sell_AddTheSamProduct_AmountShouldGrows()
        {
            Product product = new() { Name = "Product", Price = 0.1m };

            VendingMachineService vendingMachine = new VendingMachineService(new PaymentService());
            vendingMachine.AddProduct(product, 1);
            vendingMachine.AddProduct(product, 1);


            var products = vendingMachine.GetProducts();
            Assert.Single(products);
            Assert.Equal(2, products[product]);
        }

        [Fact]
        public void Sell_AddTheSamBill_AmountShouldGrows()
        {
            Bill bill1 = new() { Nominal = 0.1m };
            Bill bill2 = new() { Nominal = 0.1m };

            VendingMachineService vendingMachine = new VendingMachineService(new PaymentService());
            vendingMachine.AddBill(bill1, 1);
            vendingMachine.AddBill(bill2, 1);


            var bills = vendingMachine.GetBills();
            Assert.Single(bills);
            Assert.Equal(2, bills[bill1]);
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
