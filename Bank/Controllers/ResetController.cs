using Bank.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    public class ResetController(IBalanceService balanceService, ILogger<BalanceController> logger) : ControllerBase
    {
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            balanceService.Reset();

            return Ok();
        }
    }
}
