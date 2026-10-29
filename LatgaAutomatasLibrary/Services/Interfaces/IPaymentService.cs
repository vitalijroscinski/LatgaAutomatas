using LatgaAutomatasLibrary.Models;

namespace LatgaAutomatasLibrary.Services.Interfaces
{
    public interface IPaymentService
    {
        SellOperation PayTheRest(Dictionary<Bill, int> deposit, decimal restToPay);
    }
}
