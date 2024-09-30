namespace Bank.Models.Response
{
    public class Movement
    {
        public Account? Origin { get; set; }
        public Account? Destination { get; set; }
    }
}
