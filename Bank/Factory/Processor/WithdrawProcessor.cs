using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Repository;

namespace Bank.Factory.Processor
{
    public class WithdrawProcessor : ITransactionProcessor
    {
        public Movement? Process(Transaction transaction, IAccountRepository accountRepository)
        {
            var originBalance = accountRepository.Withdraw(transaction.Origin, transaction.Amount);

            return originBalance == 0 ? null : new Movement
            {
                Origin =  new Account
                {
                    Id = transaction.Origin,
                    Balance = originBalance
                }
            };
        }
    }
}
