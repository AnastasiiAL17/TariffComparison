using TariffComparison.Constants;
using TariffComparison.Models.Provider;

namespace TariffComparison.Helper
{
    public static class AnnualCostsHelper
    {
        public static decimal GetBasicAnnualCosts(Provider provider, decimal consumption)
        {
            decimal baseCosts = provider.BaseCost * CommonConstants.MONTH_IN_YEAR;
            decimal consumptionCosts = GetBasicBaseCosts(consumption,provider.AdditionalKwhCost);

            return baseCosts + consumptionCosts;
        }

        public static decimal GetBasicBaseCosts(decimal consumption, decimal additionalKwhCost)
        {
            return consumption * CommonHelper.ConvertToDollar(additionalKwhCost);
        }

        public static decimal GetPackagedAnnualCosts(Provider provider, decimal consumption)
        {
            decimal includedKwh = ((PackagedProvider)provider).IncludedKwh;
            if (consumption <= includedKwh)
            {
                return provider.BaseCost;
            }
            else
            {
                decimal consumptionCosts = GetPackagedAdditionalCosts(consumption, (PackagedProvider)provider);
                return provider.BaseCost + consumptionCosts;
            }
        }

        public static decimal GetPackagedAdditionalCosts(decimal consumption, PackagedProvider provider)
        {
            return consumption <= provider.IncludedKwh
                ? 0
                : (consumption - provider.IncludedKwh) * CommonHelper.ConvertToDollar(provider.AdditionalKwhCost);
        }
    }
}
