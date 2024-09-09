using Bank.Factory;
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
        var processor = TransactionProcessorFactory.CreateProcessor(transaction.Type);

        return processor.Process(transaction, accountRepository);
    }

    public void Reset()
    {
        logger.LogInformation("Resetting all accounts");

        accountRepository.Reset();
    }
}

