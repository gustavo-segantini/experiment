namespace Bank.Repository;

public class BalanceRepository : IBalanceRepository
{
    private static decimal _balance;

    public decimal GetBalance()
    {
        return _balance;
    }

    public void UpdateBalance(decimal amount)
    {
        _balance = amount;
    }
}

