using LatgaAutomatasLibrary.Models;
using LatgaAutomatasLibrary.Services;

namespace TestLatgaAutomatas.Services
{
    public class PaymentServiceTests
    {

        [Theory]
        [InlineData(0.4, 0.2, 2, 0, 0, 0, 0, 0, 0)]
        [InlineData(0.6, 0.5, 1, 0.1, 1, 0, 0, 0, 0)]
        [InlineData(0.8, 0.5, 1, 0.2, 1, 0.1, 1, 0, 0)]
        [InlineData(1, 0.5, 2, 0, 0, 0, 0, 0, 0)]
        public void PayTheRest_WhenEnoughBillsExist_ReturnsCorrectChange(decimal restToPay,
            decimal nominal1, int amount1,
            decimal nominal2, int amount2,
            decimal nominal3, int amount3,
            decimal nominal4, int amount4)
        {
            var paymentService = new PaymentService();

            var bill_10ct = new Bill {Nominal = 0.1m };
            var bill_20ct = new Bill {Nominal = 0.2m };
            var bill_50ct = new Bill {Nominal = 0.5m };

            var deposit = new Dictionary<Bill, int>
            {
                { bill_10ct, 10 },
                { bill_20ct, 10 },
                { bill_50ct, 10 }
            };

            var result = paymentService.PayTheRest(deposit, restToPay);

            Assert.True(result.Success);
            Assert.True(nominal1 == 0 || result.Rest.First(r => r.Key.Nominal == nominal1).Value == amount1);
            Assert.True(nominal2 == 0 || result.Rest.First(r => r.Key.Nominal == nominal2).Value == amount2);
            Assert.True(nominal3 == 0 || result.Rest.First(r => r.Key.Nominal == nominal3).Value == amount3);
            Assert.True(nominal4 == 0 || result.Rest.First(r => r.Key.Nominal == nominal4).Value == amount4);
        }

        [Fact]
        public void PayTheRest_PayWithNoMatchingBills_ReturnsFailure()
        {
            var paymentService = new PaymentService();

            var bill_10ct = new Bill { Nominal = 0.1m };
            var bill_20ct = new Bill { Nominal = 0.2m };

            var deposit = new Dictionary<Bill, int>
            {
                { bill_10ct, 1 },
                { bill_20ct, 1 }
            };

            var result = paymentService.PayTheRest(deposit, 0.7m);

            Assert.False(result.Success);
            Assert.Equal("Machine has no enougth bills", result.ErrorMessage);
        }
    }
}
