using LatgaAutomatasLibrary.Services;

namespace TestLatgaAutomatas.Services
{
    public class BillServiceTests
    {
        [Fact]
        public void ExchangeToBill_WhenBillExists_ReturnsMatchingBill()
        {
            var billService = new BillService();

            billService.DefineBill("10ct", 0.1m);
            billService.DefineBill("20ct", 0.2m);

            var result = billService.ExchangeToBill(0.2m);

            Assert.NotNull(result);
            Assert.Equal("20ct", result.Name);
            Assert.Equal(0.2m, result.Nominal);
        }

        [Fact]
        public void ExchangeToBill_WhenNominalDoesNotExist_ThrowsException()
        {
            var billService = new BillService();

            billService.DefineBill("10ct", 0.1m);
            billService.DefineBill("20ct", 0.2m);

            var exception = Assert.Throws<Exception>(() => billService.ExchangeToBill(0.5m));

            Assert.Equal("Bill was not found", exception.Message);
        }

        [Fact]
        public void DefineBill_WhenNominalAlreadyExists_ThrowsException()
        {
            var billService = new BillService();

            billService.DefineBill("10ct", 0.1m);

            var exception = Assert.Throws<Exception>(() => billService.DefineBill("another 10ct", 0.1m));

            Assert.Equal("Bill already is defined", exception.Message);
        }

        [Fact]
        public void DefineBill_WhenNominalIsNotPositive_ThrowsException()
        {
            var billService = new BillService();

            var exception = Assert.Throws<Exception>(() => billService.DefineBill("zero bill", 0m));

            Assert.Equal("Only positive nominals are accepted", exception.Message);
        }
    }
}
