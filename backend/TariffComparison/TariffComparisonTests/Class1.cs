using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TariffComparison.Models;
using TariffComparison.Service;
using TariffComparison.Service.Interfaces;

namespace TariffComparisonTests
{
    [TestFixture]
    public class AnnualCostsServiceTest
    {
        private AnnualCostsService _annualCostsService;

        [SetUp]
        public void SetUp()
        {
            var mockCofiguration = new Mock<IConfiguration>();
            var providerService = new ProviderService(mockCofiguration.Object);
            var mockLogger = new Mock<ILogger<AnnualCostsService>>();
            _annualCostsService = new AnnualCostsService(providerService, mockLogger.Object);

        }

        [Test]
        [TestCase("Product A", 3500, 830, 770)]
        [TestCase("Product A", 4500, 1050, 990)]
        [TestCase("Product B", 3500, 800, 0)]
        [TestCase("Product B", 4500, 950, 150)]
        public void Calculate(string name, decimal consumption, decimal annualCosts, decimal consumptionCosts)
        {
            var request = new ConsumptionRequest
            {
                ProviderName = name,
                Consumption = consumption
            };

            var result = _annualCostsService.Calculate(request);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(annualCosts, result.Data.AnnualCosts);
            Assert.AreEqual(consumptionCosts, result.Data.ConsumptionCosts);
        }
    }
}