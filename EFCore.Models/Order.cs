using System;
using System.Collections.Generic;

namespace EFCore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public Payment Payment { get; set; }

        public List<OrderDetail> Details { get; set; }
    }
}
