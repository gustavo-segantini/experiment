using Bank.Models;
using Bank.Repository;

namespace Bank.Services;

public class BalanceService(IBalanceRepository balanceRepository, ILogger<BalanceService> logger)
    : IBalanceService
{

    public decimal GetBalance()
    {
        var balance = balanceRepository.GetBalance();

        logger.LogInformation("Current balance is {balance}", balance);

        return balance;
    }

    public void UpdateBalance(Transaction transaction)
    {
        var balance = balanceRepository.GetBalance();
        var balanceUpdated = CalculateBalance(transaction, balance);

        logger.LogInformation("Updating balance from {oldBalance} to {newBalance}", balance, balanceUpdated);

        balanceRepository.UpdateBalance(balanceUpdated);
    }

    private static decimal CalculateBalance(Transaction transaction, decimal balance)
    {
        if (transaction.Type == MovementType.Credit)
        {
            balance += transaction.Amount;
        }
        else if (transaction.Type == MovementType.Debit)
        {
            balance -= transaction.Amount;
        }

        return balance;
    }
}

