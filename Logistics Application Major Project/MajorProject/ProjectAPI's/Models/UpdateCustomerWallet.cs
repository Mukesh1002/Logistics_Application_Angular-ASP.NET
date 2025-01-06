namespace ProjectAPI_s.Models
{
    public class UpdateCustomerWallet
    {
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerPassword { get; set; }

        public string? CustomerPhoneNo { get; set; }

        public string? CustomerAddress { get; set; }

        public decimal? CustomerWallet { get; set; }
    }
}
