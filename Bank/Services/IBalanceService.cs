using Bank.Models;

namespace Bank.Services
{
    public interface IBalanceService
    {
        decimal GetBalance();
        void UpdateBalance(Transaction transaction);
    }
}
