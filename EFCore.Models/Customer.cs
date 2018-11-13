using System.Collections.Generic;

namespace EFCore.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public List<Order> Orders { get; set; }

        //  public Address ShippingAddress { get; set; }

        public bool IsDeleted { get; set; }
    }
}
