namespace Bank.Repository;

public interface IBalanceRepository
{
    decimal GetBalance();
    void UpdateBalance(decimal amount);
}

