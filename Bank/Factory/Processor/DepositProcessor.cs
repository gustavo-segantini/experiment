using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Repository;

namespace Bank.Factory.Processor;

public class DepositProcessor : ITransactionProcessor
{
    public Movement Process(Transaction transaction, IAccountRepository accountRepository)
    {
        var destinationBalance = accountRepository.Deposit(transaction.Destination, transaction.Amount);

        return new Movement
        {
            Destination = new Account
            {
                Id = transaction.Destination,
                Balance = destinationBalance
            }
        };
    }
}