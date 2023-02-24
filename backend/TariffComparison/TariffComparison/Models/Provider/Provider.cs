using TariffComparison.Enum;

namespace TariffComparison.Models.Provider
{
    public abstract class Provider
    {
        public string Name { get; set; }
        public ProviderType Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal AdditionalKwhCost { get; set; }
    }
}
