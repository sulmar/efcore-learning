using System.Collections.Generic;

namespace EFCore.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public IList<ProductTag> ProducTags { get; set; }
    }
}
