namespace TariffComparison.Models
{
    public class ConsumptionResponse
    {
        public string TariffName { get; set; }

        public decimal? AnnualCosts { get; set; }

        public decimal? ConsumptionCosts { get; set; }
    }
}
