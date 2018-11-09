namespace EFCore.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public decimal Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
