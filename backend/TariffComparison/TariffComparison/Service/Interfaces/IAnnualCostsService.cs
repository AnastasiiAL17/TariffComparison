using TariffComparison.Models;

namespace TariffComparison.Service.Interfaces
{
    public interface IAnnualCostsService
    {
        ApiResponse<ConsumptionResponse> Calculate(ConsumptionRequest request);
    }
}
