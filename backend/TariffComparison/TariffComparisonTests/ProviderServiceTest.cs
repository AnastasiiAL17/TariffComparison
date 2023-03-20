using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using TariffComparison.Service;

namespace TariffComparisonTests
{
    [TestFixture]
    public class ProviderServiceTest
    {
        private ProviderService _providerService;

        [SetUp]
        public void SetUp()
        {
            var mockCofiguration = new Mock<IConfiguration>();
            _providerService = new ProviderService(mockCofiguration.Object);
        }

        [Test]
        [TestCase("Product A")]
        [TestCase("Product B")]
        public void GetProviderByName(string providerName)
        {
            var provider = _providerService.GetProviderByName(providerName);
            Assert.IsNotNull(provider);
        }

        [Test]
        public void GetProviderByName()
        {
            var providers = _providerService.GetProviders();
            Assert.IsNotNull(providers);
        }
    }
}