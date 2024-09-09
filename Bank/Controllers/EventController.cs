using Bank.Models;
using Bank.Models.Request;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bank.Controllers
{
    [ApiController]
    public class EventController(IBalanceService balanceService, ILogger<BalanceController> logger) : ControllerBase
    {
        [HttpPost("event")]
        public IActionResult PostEvent([FromBody] Transaction transaction)
        {
            var movement = balanceService.Movement(transaction);

            return movement?.Destination is null && movement?.Origin is null ?
                NotFound(0) :
                Created(string.Empty, movement);
        }
    }
}
