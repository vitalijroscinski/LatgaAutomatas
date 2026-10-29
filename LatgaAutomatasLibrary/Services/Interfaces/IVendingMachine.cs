using LatgaAutomatasLibrary.Models;

namespace LatgaAutomatasLibrary.Services.Interfaces
{
    public interface IVendingMachine
    {
        bool AddBill(Bill bill, int amount);
        bool AddProduct(Product product, int amount);
        SellOperation Sell(Dictionary<Product, int> products, Dictionary<Bill, int> payment);
    }
}
