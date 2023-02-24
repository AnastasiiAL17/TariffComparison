using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Models;
using TariffComparison.Service.Interfaces;

namespace TariffComparison.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class ConsumptionsController : ControllerBase
    {
        private const string INVALID_CONSUMPTION = "Consumption can`t be 0 or less.";
        private const string ERROR_MESSAGE = "Something went wrong: {message}.";
        private readonly IAnnualCostsService _annualCostsService;
        private readonly ILogger<ProvidersController> _logger;

        public ConsumptionsController(
            IAnnualCostsService annualCostsService,
            ILogger<ProvidersController> logger)
        {
            _annualCostsService = annualCostsService;
            _logger = logger;
        }

        [HttpPost(Name = "CalculateAnnualCosts")]
        public ApiResponse<ConsumptionResponse> CalculateAnnualCosts(ConsumptionRequest request)
        {
            try
            {
                if (request.Consumption <= 0)
                {
                    _logger.LogError(INVALID_CONSUMPTION);
                    return new ApiResponse<ConsumptionResponse>()
                    {
                        IsSuccess = false,
                        Message = INVALID_CONSUMPTION
                    };
                }

                return _annualCostsService.Calculate(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ERROR_MESSAGE, ex.Message);
                throw;
            }
        }
    }
}
