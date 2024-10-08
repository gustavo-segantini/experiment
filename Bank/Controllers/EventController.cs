﻿using Bank.Models.Request;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    public class EventController(IBalanceService balanceService, ILogger<BalanceController> logger) : ControllerBase
    {
        [HttpPost("event")]
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
