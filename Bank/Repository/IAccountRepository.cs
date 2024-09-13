namespace Bank.Repository;

public interface IAccountRepository
{
    decimal? GetBalance(string accountId);

    decimal Deposit(string accountId, decimal amount);

    decimal Withdraw(string accountId, decimal amount);

    (decimal OriginBalance, decimal DestinationBalance) Transfer(string originId, string destinationId, decimal amount);

    void Reset();
}

