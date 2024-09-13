
namespace Bank.Models.Request;

public class Transaction
{
    public MovementType Type { get; set; }

    public string? Origin { get; set; }

    public decimal Amount { get; set; }

    public string? Destination { get; set; }
}
