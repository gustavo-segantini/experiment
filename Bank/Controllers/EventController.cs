using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    public class EventController(IBalanceService balanceService, ILogger<BalanceController> logger) : ControllerBase
    {
        [HttpPost("event")]
        [ProducesResponseType(typeof(Movement), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult PostEvent([FromBody] Transaction transaction)
        {
            if (transaction is null)
            {
                logger.LogError("Transaction is required");

                return BadRequest("Transaction is required");
            }

            var movement = balanceService.Movement(transaction);

            return movement?.Destination is null && movement?.Origin is null ?
                NotFound(0) :
                Created(string.Empty, movement);
        }
    }
}
