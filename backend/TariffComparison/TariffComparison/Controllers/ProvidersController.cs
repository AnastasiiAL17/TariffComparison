using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Models.Provider;
using TariffComparison.Service.Interfaces;

namespace TariffComparison.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class ProvidersController : ControllerBase
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IProviderService _providerService;

        public ProvidersController(
            ILogger<ProvidersController> logger,
            IProviderService providerService)
        {
            _logger = logger;
            _providerService = providerService;
        }

        [HttpGet(Name = "GetProviders")]
        public List<Provider> Get()
        {
            try
            {
                return _providerService.GetProviders();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong: {message}.", ex.Message);
                throw;
            }
        }
    }
}