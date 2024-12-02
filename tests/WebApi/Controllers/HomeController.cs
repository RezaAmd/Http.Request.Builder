using Microsoft.AspNetCore.Mvc;
using WebApi.Clients;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaxBridgeClient _taxBridgeClient;
        public HomeController(ILogger<HomeController> logger,
            TaxBridgeClient taxBridgeClient)
        {
            _logger = logger;
            _taxBridgeClient = taxBridgeClient;
        }

        [HttpGet]
        public async Task<IActionResult> Send(CancellationToken cancellationToken)
        {
            await _taxBridgeClient.GetTokenAsync(cancellationToken);
            return Ok();
        }
    }
}
