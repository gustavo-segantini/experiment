namespace Bank.Repository;

public class AccountRepository : IAccountRepository
{
    private static readonly Dictionary<string, decimal> Accounts = new Dictionary<string, decimal>();

    public decimal? GetBalance(string accountId)
    {
        return Accounts.TryGetValue(accountId, out var balance) ? balance : null;
    }

    public decimal Deposit(string accountId, decimal amount)
    {
        Accounts.TryAdd(accountId, 0);

        Accounts[accountId] += amount;
        return Accounts[accountId];
    }

    public decimal Withdraw(string accountId, decimal amount)
    {
        if (!Accounts.TryGetValue(accountId, out var balance) || balance < amount)
            return 0;

        Accounts[accountId] -= amount;
        return Accounts[accountId];
    }

    public (decimal OriginBalance, decimal DestinationBalance) Transfer(string originId, string destinationId, decimal amount)
    {
        if (!Accounts.TryGetValue(originId, out var originBalance) || originBalance < amount)
            return (0, 0);

        Accounts.TryAdd(destinationId, 0);

        Accounts[originId] -= amount;
        Accounts[destinationId] += amount;

        return (Accounts[originId], Accounts[destinationId]);
    }

    public void Reset()
    {
        Accounts.Clear();
    }
}

