using System.Collections.Generic;

namespace EFCore.Models
{
    public class Product : Item
    {
        public string Barcode { get; set; }

        public string Color { get; set; }

        public IList<ProductTag> ProductTags { get; set; }
    }
}
