using Bank.Factory.Processor;
using Bank.Models;

namespace Bank.Factory;

public class TransactionProcessorFactory
{
    public static ITransactionProcessor CreateProcessor(MovementType type)
    {
        return type switch
        {
            MovementType.Withdraw => new WithdrawProcessor(),
            MovementType.Deposit => new DepositProcessor(),
            MovementType.Transfer => new TransferProcessor(),
            _ => throw new ArgumentException("Unknown transaction type", nameof(type)),
        };
    }
}