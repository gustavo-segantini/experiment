using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Repository;

namespace Bank.Factory.Processor;

public class TransferProcessor : ITransactionProcessor
{
    public Movement? Process(Transaction transaction, IAccountRepository accountRepository)
    {
        var balance = accountRepository.Transfer(transaction.Origin, transaction.Destination, transaction.Amount);

        return balance is { OriginBalance: 0, DestinationBalance: 0 }  ? null : new Movement
        {
            Origin =  new Account
            {
                Id = transaction.Origin,
                Balance = balance.OriginBalance
            },
            Destination =  new Account
            {
                Id = transaction.Destination,
                Balance = balance.DestinationBalance
            }
        };
    }
}