namespace EFCore.Models
{
    public class CustomerTotalAmount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; } // Navigation property

        public override string ToString() => $"{FirstName} {LastName} {TotalAmount}";
    }
}
