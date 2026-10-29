using LatgaAutomatas.Models;
using LatgaAutomatas.Services.Interfaces;
using System.Windows;

namespace LatgaAutomatas
{
    public partial class MainWindow : Window
    {
        private readonly IVendingMachine _vendingMachine;

        public MainWindow(IVendingMachine vendingMachine)
        {
            InitializeComponent();
            _vendingMachine = vendingMachine;
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Bill bill_10ct = new() { Name = "10ct", Nominal = 0.1m };
            Bill bill_20ct = new() { Name = "20ct", Nominal = 0.2m };
            Bill bill_50ct = new() { Name = "50ct", Nominal = 0.5m };

            Product product1 = new() { Name = "Product1", Price = 0.1m };
            Product product2 = new() { Name = "Product2", Price = 0.2m };
            Product product3 = new() { Name = "Product3", Price = 0.3m };

            _vendingMachine.AddBill(bill_10ct, 10);
            _vendingMachine.AddBill(bill_20ct, 10);
            _vendingMachine.AddBill(bill_50ct, 10);

            _vendingMachine.AddProduct(product1, 1);
            _vendingMachine.AddProduct(product2, 1);
            _vendingMachine.AddProduct(product3, 1);

            Dictionary<Product, int> bag = new() { { product1, 1} };
            Dictionary<Bill, int> cash = new() { { bill_50ct, 1} };

            var rest = _vendingMachine.Sell(bag, cash);
        }
    }
}
