using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ResetController(IBalanceService balanceService, ILogger<BalanceController> logger) : ControllerBase
    {
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            logger.LogInformation("Resetting all accounts");

            balanceService.Reset();

            return Ok();
        }
    }
}
