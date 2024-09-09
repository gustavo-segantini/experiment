namespace Bank.Models.Response
{
    public class Movement
    {
        public Account? Origin { get; set; }
        public Account? Destination { get; set; }
    }

    public class Account
    {
        public required string? Id { get; set; }
        public required decimal Balance { get; set; }
    }
}
