using TariffComparison.Models;
using TariffComparison.Models.Provider;

namespace TariffComparison.Service.Interfaces
{
    public interface IProviderService
    {
        public List<Provider> GetProviders();

        public Provider? GetProviderByName(string providerName);
    }
}
