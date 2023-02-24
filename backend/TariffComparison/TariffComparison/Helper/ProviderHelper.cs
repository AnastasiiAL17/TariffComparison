using Dynamitey;
using TariffComparison.Enum;
using TariffComparison.Models.Provider;

namespace TariffComparison.Helper
{
    public static class ProviderHelper
    {
        public static BasicProvider GetBasicProvider(dynamic provider)
        {
            return new BasicProvider
            {
                Name = Convert.ToString(Dynamic.InvokeGet(provider, "name")),
                Type = ProviderType.basic,
                BaseCost = Convert.ToDecimal(Dynamic.InvokeGet(provider, "baseCost")),
                AdditionalKwhCost = Convert.ToDecimal(Dynamic.InvokeGet(provider, "additionalKwhCost"))
            };
        }

        public static PackagedProvider GetPackagedProvider(dynamic provider)
        {
            return new PackagedProvider
            {
                Name = Convert.ToString(Dynamic.InvokeGet(provider, "name")),
                Type = ProviderType.packaged,
                BaseCost = Convert.ToDecimal(Dynamic.InvokeGet(provider, "baseCost")),
                AdditionalKwhCost = Convert.ToDecimal(Dynamic.InvokeGet(provider, "additionalKwhCost")),
                IncludedKwh = Convert.ToDecimal(Dynamic.InvokeGet(provider, "includedKwh"))
            };
        }
    }
}
