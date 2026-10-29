using LatgaAutomatas.Models;

namespace LatgaAutomatas.Services.Interfaces
{
    public interface IPaymentService
    {
        SellOperation PayTheRest(Dictionary<Bill, int> deposit, decimal restToPay);
    }
}
