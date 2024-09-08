using Bank.Models;
using Bank.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers;

public class BalanceController(IMediator mediator, IBalanceService balanceService, ILogger<BalanceController> logger) : Controller
{
    [HttpGet("balance")]
    public ActionResult<Balance> GetBalance()
    {
        var balance = balanceService.GetBalance();

        logger.LogInformation("Current balance is {balance}", balance);

        return Ok(new Balance { Amount = balance });
    }

    [HttpPost("event")]
    public IActionResult PostEvent([FromBody] Transaction transaction)
    {
        logger.LogInformation("Received event: {@transaction}", transaction);

        balanceService.UpdateBalance(transaction);

        return Ok();
    }
}