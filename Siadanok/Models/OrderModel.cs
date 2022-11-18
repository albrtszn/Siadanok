namespace Siadanok.Models
{
    public class OrderModel
    {
        public string? OrderType { get; set; }
        public string? PayMethod { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Building { get; set; }
        public string? Apartment { get; set; }
        public string? Comment { get; set; }

        public string? Table { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Wallet { get; set; }
    }
}
