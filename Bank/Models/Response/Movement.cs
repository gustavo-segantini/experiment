namespace Bank.Models.Response
{
    public class Movement
    {
        public Account? Origin { get; set; }
        public Account? Destination { get; set; }
    }

    public class Account
    {
        public string? Id { get; set; }
        public decimal Balance { get; set; }
    }
}
