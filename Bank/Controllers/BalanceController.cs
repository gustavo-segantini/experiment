using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers;

public class BalanceController(IBalanceService balanceService, ILogger<BalanceController> logger) : Controller
{
    [HttpGet("balance")]
    public IActionResult GetBalance([FromQuery] string account_id)
    {
        var balance = balanceService.GetBalance(account_id);

        if (balance == null)
        {
            return NotFound(0);
        }

        return Ok(balance);
    }
}