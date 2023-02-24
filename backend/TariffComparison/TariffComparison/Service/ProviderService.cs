using Dynamitey;
using TariffComparison.Enum;
using TariffComparison.Helper;
using TariffComparison.Models.Provider;
using TariffComparison.Service.Interfaces;

namespace TariffComparison.Service
{
    public class ProviderService : IProviderService
    {
        private const string TYPE = "type";
        private readonly IConfiguration _configuration;

        public ProviderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Provider> GetProviders()
        {
            var data = GetJsonData();

            if (data.IsNullOrEmpty())
            {
                return new List<Provider>();
            }

            return ConvertToProviders(data);
        }

        public Provider? GetProviderByName(string providerName)
        {
            List<Provider> providers = GetProviders();

            if (providers.IsNullOrEmpty())
            {
                return null;
            }

            return providers.FirstOrDefault(provider => provider.Name == providerName);
        }

        private List<dynamic>? GetJsonData()
        {
            JsonHelper jsonReader = new(_configuration);
            return jsonReader.LoadJsonData();
        }

        private static List<Provider> ConvertToProviders(List<dynamic> data)
        {
            List<Provider> providers = new();

            foreach (var provider in data)
            {
                var type = Dynamic.InvokeGet(provider, TYPE);

                if (type == ProviderType.basic)
                {
                    providers.Add(ProviderHelper.GetBasicProvider(provider));
                }
                else if (type == ProviderType.packaged)
                {
                    providers.Add(ProviderHelper.GetPackagedProvider(provider));
                }
            }

            return providers;
        }
    }
}
