using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Repository;

namespace Bank.Factory.Processor;

public class DepositProcessor : ITransactionProcessor
{
    public Movement Process(Transaction transaction, IAccountRepository accountRepository)
    {
        if (transaction.Destination == null)
        {
            throw new ArgumentNullException(nameof(transaction.Destination), "Destination account ID cannot be null.");
        }

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
