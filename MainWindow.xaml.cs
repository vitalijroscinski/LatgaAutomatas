using LatgaAutomatas.Models;
using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace LatgaAutomatas
{
    public partial class MainWindow : Window
    {
        private readonly IVendingMachine _vendingMachine;
        public ObservableCollection<BillAmount> BillAmounts { get; } = new();
        public ObservableCollection<ProductAmount> ProductAmounts { get; } = new();

        public MainWindow(IVendingMachine vendingMachine)
        {
            InitializeComponent();
            _vendingMachine = vendingMachine;
            InitializeData();
            DataContext = this;
        }

        private void InitializeData()
        {
            BillAmounts.Add(new BillAmount{ Bill = new Bill { Name = "10ct", Nominal = 0.1m }, Amount = 0 });
            BillAmounts.Add(new BillAmount{ Bill = new Bill { Name = "20ct", Nominal = 0.2m }, Amount = 0 });
            BillAmounts.Add(new BillAmount{ Bill = new Bill { Name = "50ct", Nominal = 0.5m }, Amount = 0 });
            BillAmounts.Add(new BillAmount{ Bill = new Bill { Name = "1€", Nominal = 1m }, Amount = 0 });
            BillAmounts.Add(new BillAmount{ Bill = new Bill { Name = "2€", Nominal = 2m }, Amount = 0 });
            BillAmounts.Add(new BillAmount{ Bill = new Bill { Name = "5€", Nominal = 5m }, Amount = 0 });

            ProductAmounts.Add(new ProductAmount() { Product = new() { Name = "Product1", Price = 0m }, Amount = 0 });
            ProductAmounts.Add(new ProductAmount() { Product = new() { Name = "Product2", Price = 0m }, Amount = 0 });
        }

        private void FeedMachine(object sender, RoutedEventArgs e)
        {
        }

        private void BuyProducts(object sender, RoutedEventArgs e)
        {
        }
    }
}
