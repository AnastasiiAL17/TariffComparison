using System.Text;
using TariffComparison.Enum;
using TariffComparison.Helper;
using TariffComparison.Models;
using TariffComparison.Models.Provider;
using TariffComparison.Service.Interfaces;

namespace TariffComparison.Service
{
    public class AnnualCostsService : IAnnualCostsService
    {
        private const string ERROR_MESSAGE = "Error while calculating anuual costs: {0}.";
        private const string PROVIDER_NOT_FOUND = "Provider is not found.";

        private readonly IProviderService _providerService;
        private readonly ILogger<AnnualCostsService> _logger;

        public AnnualCostsService(
            IProviderService providerService,
            ILogger<AnnualCostsService> logger)
        {
            _logger = logger;
            _providerService = providerService;
        }

        public ApiResponse<ConsumptionResponse> Calculate(ConsumptionRequest request)
        {
            try
            {
                var provider = _providerService.GetProviderByName(request.ProviderName);

                if (provider == null)
                {
                    return new ApiResponse<ConsumptionResponse>()
                    {
                        IsSuccess = false,
                        Message = PROVIDER_NOT_FOUND
                    };
                }

                return new ApiResponse<ConsumptionResponse>
                {
                    IsSuccess = true,
                    Data = new ConsumptionResponse()
                    {
                        AnnualCosts = GetAnnualCosts(provider, request),
                        TariffName = GetTariffInformation(provider),
                        ConsumptionCosts = GetBaseCosts(provider, request)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ERROR_MESSAGE, ex.Message);
                throw new Exception("title", ex);
            }
        }

        private static decimal? GetAnnualCosts(Provider provider, ConsumptionRequest request)
        {
            return provider.Type switch
            {
                ProviderType.basic => AnnualCostsHelper.GetBasicAnnualCosts(provider, request.Consumption),
                ProviderType.packaged => AnnualCostsHelper.GetPackagedAnnualCosts(provider, request.Consumption),
                _ => null,
            };
        }

        private static decimal? GetBaseCosts(Provider provider, ConsumptionRequest request)
        {
            return provider.Type switch
            {
                ProviderType.basic => AnnualCostsHelper.GetBasicBaseCosts(request.Consumption, provider.AdditionalKwhCost),
                ProviderType.packaged => AnnualCostsHelper.GetPackagedAdditionalCosts(request.Consumption, (PackagedProvider)provider),
                _ => null,
            };
        }

        private static string GetTariffInformation(Provider provider)
        {
            var tariffName = new StringBuilder("TARIFF: ");
            tariffName.AppendLine(provider.Type switch
            {
                ProviderType.basic => "Basic electricity tariff",
                ProviderType.packaged => "Packaged tariff",
                _ => string.Empty
            });

            return tariffName.ToString();
        }
    }
}
