using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Repository;

namespace Bank.Factory.Processor
{
    public interface ITransactionProcessor
    {
        Movement? Process(Transaction transaction, IAccountRepository accountRepository);
    }
}
