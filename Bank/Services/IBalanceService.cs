using Bank.Models.Request;
using Bank.Models.Response;

namespace Bank.Services
{
    public interface IBalanceService
    {
        decimal? GetBalance(string accountId);

        Movement? Movement(Transaction transaction);

        void Reset();
    }
}
