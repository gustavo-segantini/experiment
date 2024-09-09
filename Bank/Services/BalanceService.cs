using Bank.Models;
using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Repository;

namespace Bank.Services;

public class BalanceService(IAccountRepository accountRepository, ILogger<BalanceService> logger)
    : IBalanceService
{

    public decimal? GetBalance(string accountId)
    {
        var balance = accountRepository.GetBalance(accountId);

        logger.LogInformation("Balance for account {AccountId} is {Balance}", accountId, balance);

        return balance;
    }

    public Movement? Movement(Transaction transaction)
    {
        var originBalance = 0m;
        var destinationBalance = 0m;

        switch (transaction.Type)
        {
            case MovementType.Withdraw:
                originBalance = accountRepository.Withdraw(transaction.Origin, transaction.Amount);
                break;
            case MovementType.Deposit:
                destinationBalance = accountRepository.Deposit(transaction.Origin ?? transaction.Destination, transaction.Amount);
                break;
            case MovementType.Transfer:
                (originBalance, destinationBalance) = accountRepository.Transfer(transaction.Origin, transaction.Destination, transaction.Amount);
                break;
            default:
                logger.LogWarning("Unknown transaction type {TransactionType}", transaction.Type);

                return null;
        }

        return new Movement
        {
            Origin = originBalance == 0 ? null : new Account
            {
                Id = transaction.Destination ?? transaction.Origin,
                Balance = originBalance
            },
            Destination = destinationBalance == 0m ? null : new Account
            {
                Id = transaction.Destination ?? transaction.Origin,
                Balance = destinationBalance
            }
        };
    }

    public void Reset()
    {
        logger.LogInformation("Resetting all accounts");

        accountRepository.Reset();
    }
}

