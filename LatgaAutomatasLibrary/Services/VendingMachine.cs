using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services.Interfaces;

namespace LatgaAutomatasLibrary.Services
{
    public class VendingMachine : IVendingMachine
    {
        private readonly IPaymentService _paymentService;
        private readonly Dictionary<Bill, int> _machineBills = new();
        private readonly Dictionary<Product, int> _machineProducts = new();

        public VendingMachine(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        private bool AddItem<T>(Dictionary<T, int> items, T item, int amount) where T : notnull
        {
            if (item is null)
                return false;

            if (items.ContainsKey(item))
                items[item] += amount;
            else
                items.Add(item, amount);

            return true;
        }

        public bool AddBill(Bill bill, int amount) => AddItem(_machineBills, bill, amount);
        public bool AddProduct(Product product, int amount) => AddItem(_machineProducts, product, amount);

        public SellOperation Sell(Dictionary<Product, int> orderedProductAmounts, Dictionary<Bill, int> payment)
        {
            var productsSummaryPrice = orderedProductAmounts.Sum(x => x.Key.Price * x.Value);
            var paymentCash = payment.Sum(x => x.Key.Nominal * x.Value);

            if (paymentCash < productsSummaryPrice)
            {
                return new SellOperation
                {
                    Success = false,
                    ErrorMessage = "Insufficient payment amount."
                };
            }

            var missingProducts = orderedProductAmounts.Select(p => p.Key).Except(_machineProducts.Keys);
            if (missingProducts.Any())
            {
                return new SellOperation
                {
                    Success = false,
                    ErrorMessage = "Some products are missing: " + string.Join(", ", missingProducts.Select(p => p.Name)) + "."
                };
            }

            var missingAmounts = orderedProductAmounts.Where(a => _machineProducts[a.Key] < a.Value);
            if (missingAmounts.Any())
            {
                return new SellOperation
                {
                    Success = false,
                    ErrorMessage = "Not sufficent amount of this products: " + string.Join(", ", missingAmounts.Select(a => a.Key.Name))
                };
            }

            decimal rest = paymentCash - productsSummaryPrice;
            if (rest == 0)
            {
                return new SellOperation
                {
                    Success = true,
                    Rest = new Dictionary<Bill, int>()
                };
            }

            var restOperation = _paymentService.PayTheRest(_machineBills, rest);
            return restOperation;

        }
    }
}
