using LatgaAutomatas.Models;
using LatgaAutomatas.Services.Interfaces;

namespace LatgaAutomatas.Services
{
    public class PaymentService : IPaymentService
    {
        public SellOperation PayTheRest(Dictionary<Bill, int> deposit, decimal restToPay)
        {
            if (restToPay == 0)
                return new()
                {
                    Success = true,
                    Rest = new()
                };

            var bill = deposit
                .Where(x => x.Value > 0 && x.Key.Nominal <= restToPay)
                .OrderByDescending(x => x.Key.Nominal)
                .FirstOrDefault();

            if (bill.Key is null)
                return new()
                {
                    Success = false,
                    ErrorMessage = "Machine has no enougth bills"
                };

            restToPay -= bill.Key.Nominal;
            deposit[bill.Key] -= 1;

            var rest = PayTheRest(deposit, restToPay);
            if (!rest.Success)
                return rest;

            if (rest.Rest.ContainsKey(bill.Key))
                rest.Rest[bill.Key] += 1;
            else
                rest.Rest[bill.Key] = 1;

            return rest;
        }
    }
}
